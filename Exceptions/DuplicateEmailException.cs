using System.Runtime.Serialization;

namespace Dotnet6_API.Exceptions
{
    [Serializable]
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(string? message = null)
           : base(message ?? "There is already an account with this email")
        { }

        public DuplicateEmailException(string message, Exception innerException)
        : base(message, innerException)
        { }

        protected DuplicateEmailException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
