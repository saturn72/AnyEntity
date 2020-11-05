using Messagee.API.Domain;
using Messagee.API.Services.Security;
using Messagee.API.Services.Topics;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Collections.Generic;
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
            pm.Setup(p => p.PermittedForTopics(It.IsAny<IEnumerable<TopicPermissionRecord>>())).ReturnsAsync(false);

            var srv = new TopicService(pm.Object, null, logger.Object);

            var res = await srv.GetRegistrationToken(null);
            res.ShouldBeNull();
        }
        [Fact]
        public async Task GetTopicIdsByTopicNames_GeneratesCode()
        {
            var logger = new Mock<ILogger<TopicService>>();
            var pm = new Mock<IPermissionManager>();
            pm.Setup(p => p.PermittedForTopics(It.IsAny<IEnumerable<TopicPermissionRecord>>())).ReturnsAsync(true);

            var token = "t";
            var tg = new Mock<ITokenGenerator>();
            tg.Setup(t => t.Next()).ReturnsAsync(token);
            var srv = new TopicService(pm.Object, tg.Object, logger.Object);

            var res = await srv.GetRegistrationToken(null);
            res.ShouldBe(token);
        }
    }
}
