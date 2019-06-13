using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnyEntity.Controllers
{
    public class AnyEntityController : ControllerBase
    {
        public async Task<IActionResult> Get(string id)
        {
            return await Task.FromResult(new OkObjectResult(id));
        }
    }
}