using System;
using System.Collections.Generic;
using System.Net;
using KP.GmailApi.Models;

namespace KP.GmailApi.Common
{
    /// <summary>
    /// An error from the Gmail API service
    /// </summary>
    public class GmailException : Exception
    {
        /// <summary>
        /// The <see cref="HttpStatusCode"/> returned by Gmail
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }
        /// <summary>
        /// The errors returned by Gmail if any
        /// </summary>
        public List<GmailError> Errors { get; private set; }

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