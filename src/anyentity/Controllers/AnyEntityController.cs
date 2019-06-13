using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnyEntity.Controllers
{
    [Route("{entityName}")]
    public class AnyEntityController : ControllerBase
    {
        // private readonly DbContext _dbContext;
        private readonly WorkContext _workContext;

        // private string _entityName;
        //     _entityName = RouteData.Values["entityName"].ToString();

        public AnyEntityController(/* DbContext dbContext,*/ WorkContext workContext)
        {
            // _dbContext = dbContext;
            _workContext = workContext;
            // _dbSet = _dbContext.Set(_workContext.entityType);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]object entity)
        {
            // await _dbContext.AddAsync(entity);
            var r = Request.QueryString;
            return await Task.FromResult(new OkObjectResult("post: " + entity.ToString()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return await Task.FromResult(new OkObjectResult("get by id: " + id));
        }
        [HttpGet("filter/{filter}")]
        public async Task<IActionResult> GetAll(string filter)
        {
            return await Task.FromResult(new OkObjectResult("get and filter: " + filter));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]object entity)
        {
            return await Task.FromResult(new OkObjectResult("put by id: " + id));
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody]object entity)
        {

            return await Task.FromResult(new OkObjectResult("patch: " + entity.ToString()));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await Task.FromResult(new OkObjectResult("delete by id: " + id));
        }
    }
}