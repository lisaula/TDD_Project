using System;
using System.Runtime.Serialization;

namespace ConsoleApp
{
    [Serializable]
    internal class ConnectionCloseException : Exception
    {
        public ConnectionCloseException()
        {
        }

        public ConnectionCloseException(string message) : base(message)
        {
        }

        public ConnectionCloseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConnectionCloseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}