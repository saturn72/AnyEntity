using Xunit;
using System.Threading.Tasks;
using Shouldly;
using Messagee.API.Models;
using Messagee.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Messagee.API.Tests.Controllers
{
    public class ConfigControllerTests
    {
        [Fact]
        public async Task Post_InvalidModelState()
        {
            var ctrl = new ConfigController();
            ctrl.ModelState.AddModelError("key", "error message");
            var res = await ctrl.Post(new ConfigData());
            res.ShouldBeOfType<BadRequestObjectResult>();

        }
        [Fact]
        public async Task Post_NullModel()
        {
            var ctrl = new ConfigController();
            ctrl.ModelState.AddModelError("key", "error message");
            var res = await ctrl.Post(null);
            res.ShouldBeOfType<BadRequestObjectResult>();

        }
    }
}