using Xunit;
using System.Threading.Tasks;
using Shouldly;
using Messagee.API.Models;
using Messagee.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Messagee.API.Services.Topics;
using Messagee.API.Domain;

namespace Messagee.API.Tests.Controllers
{
    public class ConfigControllerTests
    {
        [Fact]
        public async Task Post_InvalidModelState()
        {
            var logger = new Mock<ILogger<RegistrationController>>();

            var ctrl = new RegistrationController(null, logger.Object);
            ctrl.ModelState.AddModelError("key", "error message");
            var res = await ctrl.Post(new RegistrationModel());
            res.ShouldBeOfType<BadRequestResult>();
        }
        [Fact]
        public async Task Post_InvalidBadTopicNames()
        {
            var tSrv = new Mock<ITopicService>();
            tSrv.Setup(t => t.GetRegistrationToken(It.IsAny<IEnumerable<TopicPermissionRecord>>()))
                .ReturnsAsync(null as string);

            var logger = new Mock<ILogger<RegistrationController>>();
            var ctrl = new RegistrationController(tSrv.Object, logger.Object);
            var cm = new RegistrationModel { Topics = new[] { new TopicRegistrationRequest() } };
            var res = await ctrl.Post(cm);
            res.ShouldBeOfType<BadRequestResult>();
        }
        [Fact]
        public async Task Post_REturnsRegistrationData()
        {
            var token = "token";
            var tSrv = new Mock<ITopicService>();
            tSrv.Setup(t => t.GetRegistrationToken(It.IsAny<IEnumerable<TopicPermissionRecord>>()))
                .ReturnsAsync(token);

            var logger = new Mock<ILogger<RegistrationController>>();
            var ctrl = new RegistrationController(tSrv.Object, logger.Object);
            var cm = new RegistrationModel { Topics = new[] { new TopicRegistrationRequest() } };
            var res = await ctrl.Post(cm);
            var ok = res.ShouldBeOfType<OkObjectResult>();
            ok.Value.ShouldBe(token);
        }
    }
}