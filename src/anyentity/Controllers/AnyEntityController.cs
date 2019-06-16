using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnyEntity.Controllers
{
    [Route("{entityName}")]
    public class AnyEntityController : ControllerBase
    {
        private readonly IQueryable<object> _queryable;
        public AnyEntityController(IQueryable<object> queryable)
        {
            _queryable = queryable;
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
            var g = _queryable.Where(c => ExtractById(id, c));

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
            var g = _queryable.Where(c => ExtractById(id, c));
            return await Task.FromResult(new OkObjectResult("put by id: " + id));
        }

        private bool ExtractById(string id, object c)
        {
            return id.Length % 2 == 0;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody]object entity)
        {
            var g = _queryable.Where(c => ExtractById(id, c));

            return await Task.FromResult(new OkObjectResult("patch: " + entity.ToString()));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var g = _queryable.Where(c => ExtractById(id, c));

            return await Task.FromResult(new OkObjectResult("delete by id: " + id));
        }
    }
}