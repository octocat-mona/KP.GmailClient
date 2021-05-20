using System;

namespace KP.GmailClient.Authentication
{
    public class GoogleAuthorizationException : Exception
    {
        public GoogleAuthorizationException()
        {
        }

        public GoogleAuthorizationException(string message) : base(message)
        {
        }

        public GoogleAuthorizationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
