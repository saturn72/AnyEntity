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
            TopicPermissionTypes.All.Count().ShouldBe(2);
            TopicPermissionTypes.Publisher.ShouldBe("pub");
            TopicPermissionTypes.Subscriber.ShouldBe("sub");
        }
    }
}
