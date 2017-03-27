using System;

namespace SubPointSolutions.Shelly.Core.Exceptions
{
    [Serializable]
    public class ShAppException : System.Exception
    {
        public ShAppException() { }
        public ShAppException(string message) : base(message) { }
        public ShAppException(string message, System.Exception inner) : base(message, inner) { }
        protected ShAppException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
