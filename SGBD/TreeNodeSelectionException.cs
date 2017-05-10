using System;
using System.Runtime.Serialization;

namespace SGBD
{
    [Serializable]
    internal class TreeNodeSelectionException : Exception
    {
        public TreeNodeSelectionException()
        {
        }

        public TreeNodeSelectionException(string message) : base(message)
        {
        }

        public TreeNodeSelectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TreeNodeSelectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}