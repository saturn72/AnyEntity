using Xunit;
using Messangee.API.Controllers;
using Messangee.API.Models;
using System.Threading.Tasks;
using Shouldly;
using Microsoft.AspNetCore.Mvc;

namespace Messangee.API.Tests.Controllers
{
    public class ConfigControllerTests
    {
        [Fact]
        public async Task Post_InvalidModelState()
        {
            var ctrl = new ConfigController();
            ctrl.ModelState.AddModelError("key", "error message");
            var res = await ctrl.Post(new ConfigModel());
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