using System;
using System.Collections.Generic;
using System.Net;
using KP.GmailClient.Models;

namespace KP.GmailClient.Common
{
    /// <summary>
    /// An error from the Gmail API service
    /// </summary>
    public class GmailException : Exception
    {
        /// <summary>
        /// The <see cref="HttpStatusCode"/> returned by Gmail
        /// </summary>
        public HttpStatusCode StatusCode { get; }
        /// <summary>
        /// The errors returned by Gmail if any
        /// </summary>
        public List<GmailError> Errors { get; }

        internal GmailException(GmailErrorResponse errorResponse, Exception innerException)
            : base(ConstructMessage(errorResponse), innerException)
        {
            StatusCode = (HttpStatusCode)errorResponse.Code;
            Errors = errorResponse.Errors;
        }

        internal GmailException(GmailErrorResponse errorResponse)
            : this(errorResponse, null)
        {
        }

        internal GmailException(HttpStatusCode statusCode, string message, Exception innerException)
            : base(ConstructMessage(statusCode, message), innerException)
        {
            StatusCode = statusCode;
        }

        internal GmailException(HttpStatusCode statusCode, string message)
            : this(statusCode, message, null)
        {
        }

        private static string ConstructMessage(GmailErrorResponse errorResponse)
        {
            return string.Concat(errorResponse.Code, ": ", errorResponse.Message);
        }

        private static string ConstructMessage(HttpStatusCode statusCode, string message)
        {
            return string.Concat(statusCode, ":", message);
        }
    }
}