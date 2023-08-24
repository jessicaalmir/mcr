using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace mcr.Data.DTO
{
    public class BaseMessage<T> where T : class
    {
        public string Message {get;set; } = "";
        public HttpStatusCode StatusCode {get;set; }
        public int TotalElements {get; set;} = 0;   
        public List<T> Elements {get; set;} = new List<T>();
    }
}