using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr.Data.DTO
{
    public class BaseMessageStatus
    {
        public const string OK_200 = "200 OK";
        public const string BAD_REQUEST_400 = "400 Bad Request";
        public const string INTERNAL_SERVER_ERROR_500 = "500 Internal Server Error";
        public const string NOT_FOUND_404 = "404 Not Found";
    }
}