using System;
using System.Runtime.Serialization;

namespace DataAccess.Exceptions
{
    [Serializable]
    public class ObjectDoesNotExistException : Exception
    {
        public ObjectDoesNotExistException()
        {
        }

        public ObjectDoesNotExistException(string message) : base(message)
        {
        }

        public ObjectDoesNotExistException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ObjectDoesNotExistException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
