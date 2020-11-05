using Messagee.API.Services.Topics;
using Shouldly;
using System.Linq;
using Xunit;

namespace Messagee.API.Tests.Services.Topics
{
    public class TopicRegistrationTypesTests
    {
        [Fact]
        public void TopicRegistrationTypes_AllTests()
        {
            TopicRegistrationTypes.All.Count().ShouldBe(2);
            TopicRegistrationTypes.Publisher.ShouldBe("pub");
            TopicRegistrationTypes.Subscriber.ShouldBe("sub");
        }
    }
}
