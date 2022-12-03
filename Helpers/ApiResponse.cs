using System;
using System.Collections.Generic;

namespace TWP_API_Log.Helpers
{
    public class ApiResponse
    {
        public string statusCode{get;set;}
        public object message{get;set;}
        public IEnumerable<string> error{get;set;}
        public object data{get;set;}

    }
}