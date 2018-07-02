using System;
namespace Panther.Clients
{
    public class ApiException : Exception
    {
        public ApiException(int statusCode)
        {
            HttpStatusCode = statusCode;
        }

        public int HttpStatusCode { get; private set; }
    }
}
