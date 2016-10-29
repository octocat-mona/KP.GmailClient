/*
Copyright (c) 2000  JavaScience Consulting,  Michel Gallant

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KP.GmailClient
{
    /// <summary>
    /// OpenSSL Public and Private Key Parser.
    ///  Reads and parses:
    ///    (1) OpenSSL PEM or DER public keys
    ///    (2) OpenSSL PEM or DER traditional SSLeay private keys (encrypted and unencrypted)
    ///    (3) PKCS #8 PEM or DER encoded private keys (encrypted and unencrypted)
    ///  Keys in PEM format must have headers/footers .
    ///  Encrypted Private Key in SSLEay format not supported in DER
    ///  Removes header/footer lines.
    ///  For traditional SSLEAY PEM private keys, checks for encrypted format and
    ///  uses PBE to extract 3DES key.
    ///  For SSLEAY format, only supports encryption format: DES-EDE3-CBC
    ///  For PKCS #8, only supports PKCS#5 v2.0  3des.
    ///  Parses private and public key components and returns .NET RSA object.
    /// </summary>
    internal class Opensslkey
    {
        /// <summary>
        /// Decode PEM pubic, private or pkcs8 key.
        /// </summary>
        /// <param name="pemKey"></param>
        /// <returns></returns>
        public static RSACryptoServiceProvider GetRsaFromPemKey(string pemKey)
        {
            var pkcs8PrivateKey = DecodePkcs8PrivateKey(pemKey);
            if (pkcs8PrivateKey == null)
            {
                throw new Exception("Can't decode PEM key");
            }

            RSACryptoServiceProvider rsa = DecodePrivateKeyInfo(pkcs8PrivateKey);
            if (rsa == null)
            {
                throw new Exception("Failed to create an RSACryptoServiceProvider");

            }

            return rsa;
        }

        //--------   Get the binary PKCS #8 PRIVATE key   --------
        private static byte[] DecodePkcs8PrivateKey(string instr)
        {
            const string pemp8Header = "-----BEGIN PRIVATE KEY-----";
            const string pemp8Footer = "-----END PRIVATE KEY-----";
            string pemstr = instr.Trim();
            byte[] binkey;
            if (!pemstr.StartsWith(pemp8Header) || !pemstr.EndsWith(pemp8Footer))
            {
                return null;
            }

            StringBuilder sb = new StringBuilder(pemstr);
            sb.Replace(pemp8Header, "");  //remove headers/footers, if present
            sb.Replace(pemp8Footer, "");

            string pubstr = sb.ToString().Trim();   //get string after removing leading/trailing whitespace

            try
            {
                binkey = Convert.FromBase64String(pubstr);
            }
            catch (FormatException)
            {
                //if can't b64 decode, data is not valid
                return null;
            }
            return binkey;
        }

        //------- Parses binary asn.1 PKCS #8 PrivateKeyInfo; returns RSACryptoServiceProvider ---
        private static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            // this byte[] includes the sequence byte and terminal encoded null 
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            MemoryStream mem = new MemoryStream(pkcs8);
            int lenstream = (int)mem.Length;
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading

            try
            {
                using (binr)
                {
                    var twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    {
                        binr.ReadByte();    //advance 1 byte
                    }
                    else if (twobytes == 0x8230)
                    {
                        binr.ReadInt16();   //advance 2 bytes
                    }
                    else
                    {
                        return null;
                    }


                    var bt = binr.ReadByte();
                    if (bt != 0x02)
                    {
                        return null;
                    }

                    twobytes = binr.ReadUInt16();

                    if (twobytes != 0x0001)
                    {
                        return null;
                    }

                    seq = binr.ReadBytes(15);       //read the Sequence OID
                    if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
                    {
                        return null;
                    }

                    bt = binr.ReadByte();
                    if (bt != 0x04) //expect an Octet string 
                    {
                        return null;
                    }

                    bt = binr.ReadByte();       //read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count
                    switch (bt)
                    {
                        case 0x81:
                            binr.ReadByte();
                            break;
                        case 0x82:
                            binr.ReadUInt16();
                            break;
                    }
                    //------ at this stage, the remaining sequence should be the RSA private key

                    byte[] rsaprivkey = binr.ReadBytes((int)(lenstream - mem.Position));
                    RSACryptoServiceProvider rsacsp = DecodeRsaPrivateKey(rsaprivkey);
                    return rsacsp;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        //------- Parses binary ans.1 RSA private key; returns RSACryptoServiceProvider  ---
        private static RSACryptoServiceProvider DecodeRsaPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            //wrap Memory Stream with BinaryReader for easy reading

            using (MemoryStream mem = new MemoryStream(privkey))
            using (BinaryReader binr = new BinaryReader(mem))
            {
                byte bt = 0;
                ushort twobytes = 0;
                int elems = 0;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                {
                    return null;
                }

                //------  all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);


                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                RSAParameters rsaParams = new RSAParameters
                {
                    Modulus = MODULUS,
                    Exponent = E,
                    D = D,
                    P = P,
                    Q = Q,
                    DP = DP,
                    DQ = DQ,
                    InverseQ = IQ
                };

                rsa.ImportParameters(rsaParams);
                return rsa;
            }
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            int count;
            var bt = binr.ReadByte();
            //expect integer
            if (bt != 0x02)
            {
                return 0;
            }

            bt = binr.ReadByte();

            switch (bt)
            {
                case 0x81:
                    count = binr.ReadByte();    // data size in next byte
                    break;
                case 0x82:
                    var highbyte = binr.ReadByte();
                    var lowbyte = binr.ReadByte();
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                    count = BitConverter.ToInt32(modint, 0);
                    break;
                default:
                    count = bt;     // we already have the data size
                    break;
            }

            while (binr.ReadByte() == 0x00)
            {   //remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);       //last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }
    }
}
