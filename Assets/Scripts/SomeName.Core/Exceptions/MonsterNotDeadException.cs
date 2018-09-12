using System;
using System.Runtime.Serialization;

namespace SomeName.Core.Exceptions
{
    [Serializable]
    internal class MonsterNotDeadException : Exception
    {
        public MonsterNotDeadException()
        {
        }

        public MonsterNotDeadException(string message) : base(message)
        {
        }

        public MonsterNotDeadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MonsterNotDeadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}