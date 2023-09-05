using System;
using System.Collections.Generic;

namespace DemoWebAPI.DTOs.Generic
{
    public class Results<T>
    {
        public bool IsSuccess { get; set; }
        public List<T> Data { get; set; }
        public Error Error { get; set; }
        public string Message { get; set; }
        public string ResponseDate { get; set; } = DateTime.UtcNow.ToLocalTime().ToString();
    }
}
