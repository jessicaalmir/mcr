using System.Net;
using mcr.Data.Dto;

namespace mcr.Business;

public class Utilities
{
    public static BaseMessage<T> BuildResponse<T>(HttpStatusCode statusCode, string message, List<T>? elements = null)
    where T : class    
    {
        return new BaseMessage<T>(){
            StatusCode = statusCode,
            Message = message,
            TotalElements = (elements != null && elements.Any()) ? elements.Count : 0,
            Elements = elements ?? new List<T>()
        };
    }
}
