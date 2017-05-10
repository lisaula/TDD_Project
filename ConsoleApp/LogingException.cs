using System;
using System.Runtime.Serialization;

namespace ConsoleApp
{
    [Serializable]
    internal class LogingException : Exception
    {
        public LogingException()
        {
        }

        public LogingException(string message) : base(message)
        {
        }

        public LogingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LogingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}