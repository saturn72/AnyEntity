using Messagee.API.Services.Security;
using Messagee.API.Services.Topics;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
namespace Messagee.API.Tests.Services.Topics
{
    public class TopicServiceTests
    {
        [Fact]
        public async Task GetTopicIdsByTopicNames_UserNotPermitted_ReturnsNull()
        {
            var logger = new Mock<ILogger<TopicService>>();
            var pm = new Mock<IPermissionManager>();
            pm.Setup(p => p.UserPermittedForNamespace(It.IsAny<string>())).ReturnsAsync(false);

            var srv = new TopicService(pm.Object, logger.Object);
            var res = await srv.GetTopicIdsByTopicNames(null, null);
            res.ShouldBeNull();
        }
    }
}
