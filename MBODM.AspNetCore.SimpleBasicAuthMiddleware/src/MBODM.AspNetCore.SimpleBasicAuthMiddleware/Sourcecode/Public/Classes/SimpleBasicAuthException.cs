using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBODM.AspNetCore.SimpleBasicAuthMiddleware
{
    public class SimpleBasicAuthException : Exception
    {
        public SimpleBasicAuthException() { }
        public SimpleBasicAuthException(string message) : base(message) { }
        public SimpleBasicAuthException(string message, Exception inner) : base(message, inner) { }
    }
}
