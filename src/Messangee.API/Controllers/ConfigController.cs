using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Messangee.API.Models;

namespace Messangee.API.Controllers
{
    [ApiController]
    [Route("config")]
    public class ConfigController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConfigModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(new
                {
                    message = "Bad or missing data",
                    data = model
                });
            throw new NotImplementedException();
        }
        [HttpGet]
        public Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }
    }
}
