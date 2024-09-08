using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ohana.MessageLibrary
{
    public class ReturnMessage
    {
        public ReturnMessage(string message, bool success)
        {
            Message = message;
            Success = success;
        }

        public ReturnMessage(object response, string message, bool success)
        {
            Response = response;
            Message = message;
            Success = success;
        }

        public Object? Response { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
