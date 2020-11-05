using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Messagee.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Messagee.API.Services.Security;
using Messagee.API.Services.Topics;

namespace Messagee.API.Controllers
{
    [ApiController]
    [Route("register")]
    [Authorize(Roles = "system-user")]
    public class RegistrationController : ControllerBase
    {
        #region fields
        private readonly ITopicService _topicService;
        private readonly ILogger<RegistrationController> _logger;
        #endregion
        #region ctor
        public RegistrationController(
            ITopicService topicService,
            ILogger<RegistrationController> logger)
        {
            _topicService = topicService;
            _logger = logger;
        }
        #endregion

        [HttpPost]
        [Authorize(Roles = "register-create")]
        public async Task<IActionResult> Post([FromBody] RegistrationModel model)
        {
            _logger.LogInformation($"Start {nameof(Post)} using model: {model.ToJsonString()}");
            if (!ModelState.IsValid)
            {
                _logger.LogDebug("User not permitted or invalid model");
                return BadRequest();
            }
            var topicIds = await _topicService.GetTopicIdsByTopicNames(model.Namespace, model.Registrations);
            return topicIds == null ?
                BadRequest() as IActionResult :
                new OkObjectResult(topicIds);
        }
    }
}
