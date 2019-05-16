using System;

namespace BLL
{
    public class HbrException : Exception
    {
        public HbrException(string message): base(message) { }
    }
}
