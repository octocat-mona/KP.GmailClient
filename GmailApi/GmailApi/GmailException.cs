using System;
using System.Net;
using GmailApi.Models;

namespace GmailApi
{
    internal class GmailException : Exception
    {
        public ErrorResponse Error { get; set; }
        public HttpStatusCode StatusCode { get; private set; }

        public GmailException(ErrorResponse errorResponse, Exception innerException)
            : base(errorResponse.Message, innerException)
        {
            StatusCode = (HttpStatusCode)errorResponse.Code;
            Error = errorResponse;
        }

        public GmailException(ErrorResponse errorResponse)
            : base(errorResponse.Message)
        {
            StatusCode = (HttpStatusCode)errorResponse.Code;
            Error = errorResponse;
        }
    }
}