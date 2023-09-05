using System;

namespace DemoWebAPI.DTOs.Generic
{
    public class Result<T> 
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public Error Error { get; set; }
        public string Message { get; set; }
        public string ResponseDate { get; set; } = DateTime.UtcNow.ToLocalTime().ToString();
    }
}
