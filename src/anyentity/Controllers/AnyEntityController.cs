using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AnyEntity.Controllers
{
    [Route("{entityName}")]
    public class AnyEntityController : ControllerBase
    {
        private readonly AnyEntityDbContext _dbContext;
        private readonly WorkContext _workContext;

        public AnyEntityController(AnyEntityDbContext dbContext, WorkContext workContext)
        {
            _dbContext = dbContext;
            _workContext = workContext;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]IEnumerable<object> entities)
        {
            var toCreate = entities.Select(x => (x as JObject).ToObject(_workContext.EntityType)).ToArray();
            await _dbContext.AddRangeAsync(toCreate);
            return new OkObjectResult(toCreate);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var entities = await _dbContext.FindAsync(_workContext.EntityType, id);
            return new OkObjectResult(entities);
        }
        [HttpGet("filter/{filter}")]
        public async Task<IActionResult> GetAll(string filter)
        {
            var entities = _dbContext.Set(_workContext.EntityType);
            return new OkObjectResult(entities);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]IEnumerable<object> entities)
        {
            await Task.Run(() => _dbContext.UpdateRange(entities));
            return new OkObjectResult(entities);
        }
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody]object entity)
        {
            throw new System.NotImplementedException();
        }
        [HttpDelete("{ids}")]
        public async Task<IActionResult> Delete(IEnumerable<object> ids)
        {
            throw new System.NotImplementedException();
        }
    }
}