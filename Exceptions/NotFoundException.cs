using System.Runtime.Serialization;

namespace EsercitazioneAPI.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException(string? message = null)
            :base(message ?? "Not Found")
        { }

        public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
        { }

        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
