using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Messagee.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Messagee.API.Services.Topics;
using System.Collections.Generic;
using Messagee.API.Domain;
using System.Linq;

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
            var topics = ToTopicPermissionRecord(model.Topics.ToArray());
            var token = await _topicService.GetRegistrationToken(topics);
            return token == default ?
                BadRequest() as IActionResult :
                new OkObjectResult(token);
        }

        private static IEnumerable<TopicPermissionRecord> ToTopicPermissionRecord(TopicRegistrationRequest[] requests)
        {
            var length = requests.Count();

            var res = new TopicPermissionRecord[length];
            for (int i = 0; i < length; i++)
            {
                var cur = requests[i];
                res[i] = new TopicPermissionRecord
                {
                    Account = cur.Account,
                    Namespace = cur.Namespace,
                    Topic = cur.Topic,
                    PermissionType = cur.PermissionType
                };
            }
            return res;
        }
    }
}
