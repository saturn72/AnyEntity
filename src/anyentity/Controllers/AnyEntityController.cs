using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnyEntity.Controllers
{
    [Route("")]
    public class AnyEntityController : ControllerBase
    {
        // private string entityName;

        // public AnyEntityController()
        // {
        //     entityName = (string)this.RouteData.Values["controller"];
        // }
        [HttpPost("{entityName}")]
        public async Task<IActionResult> Post([FromBody]object entity)
        {
            return await Task.FromResult(new OkObjectResult("post: " + entity.ToString()));
        }
        [HttpGet("{entityName}/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return await Task.FromResult(new OkObjectResult("get by id: " + id));
        }
        [HttpGet("{entityName}/filter/{filter}")]
        public async Task<IActionResult> GetAll(string filter)
        {
            return await Task.FromResult(new OkObjectResult("get and filter: " + filter));
        }
        [HttpPut("{entityName}/{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]object entity)
        {
            return await Task.FromResult(new OkObjectResult("put by id: " + id));
        }
        [HttpPatch("{entityName}/{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody]object entity)
        {
            return await Task.FromResult(new OkObjectResult("patch: " + entity.ToString()));
        }
        [HttpDelete("{entityName}/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await Task.FromResult(new OkObjectResult("delete by id: " + id));
        }
    }
}