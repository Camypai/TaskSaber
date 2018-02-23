using System;
using System.Runtime.Serialization;

namespace TaskSaber
{
    public class ListException : Exception
    {
        public ListException()
        {
        }

        public ListException(string message) : base(message)
        {
        }

        public ListException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ListException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}