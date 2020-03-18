using Shouldly;
using Xunit;

namespace Messagee.API.Services
{
    public class LoggingEventsTests
    {
        [Fact]
        public void AllFields()
        {
            var cur = LoggingEvents.WorkContext;
            cur.Id.ShouldBe(1);
            cur.Name.ShouldBe("workcontext");

        }
    }
}