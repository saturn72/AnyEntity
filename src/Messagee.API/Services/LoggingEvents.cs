using Microsoft.Extensions.Logging;

namespace Messagee.API.Services
{
    public class LoggingEvents
    {
        public static readonly EventId WorkContext = new EventId(1, "workcontext");
    }
}