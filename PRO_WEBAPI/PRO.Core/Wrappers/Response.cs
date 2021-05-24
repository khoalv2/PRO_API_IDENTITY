using System;
using System.Collections.Generic;
using System.Text;

namespace PRO.Core.Wrappers
{
   public class Response<T>
    {
        public Response()
        {

        }

        public Response(T data)
        {
            Error = null;
            Data = data;
            Successed = false;
            Message = null;
        }

        public string Error { get; set; }
        public T Data { get; set; }
        public bool Successed { get; set; }
        public string Message { get; set; }

    }
}
