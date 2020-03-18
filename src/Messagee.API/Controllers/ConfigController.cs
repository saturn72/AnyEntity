using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Messagee.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Messagee.API.Controllers
{
    [ApiController]
    [Route("config")]
    public class ConfigController : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "config-create")]
        public async Task<IActionResult> Post([FromBody] ConfigData model)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(new
                {
                    message = "Bad or missing data",
                    data = model
                });
            throw new NotImplementedException();
        }
    }
}
