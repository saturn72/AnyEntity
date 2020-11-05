using Messagee.API.Domain;
using Messagee.API.Services.Security;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messagee.API.Services.Topics
{
    public class TopicService : ITopicService
    {
        #region fields
        private readonly IPermissionManager _permissionManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ILogger<TopicService> _logger;
        #endregion

        #region ctor
        public TopicService(
            IPermissionManager permissionManager,
            ITokenGenerator tokenGenerator,
            ILogger<TopicService> logger)
        {
            _permissionManager = permissionManager;
            _tokenGenerator = tokenGenerator;
            _logger = logger;
        }
        #endregion

        public async Task<object> GetRegistrationToken(IEnumerable<TopicPermissionRecord> records)
        {
            _logger.LogInformation($"Start {nameof(GetRegistrationToken)} with params: {nameof(records)} = {records.ToJsonString()}");
            if (!await _permissionManager.PermittedForTopics(records))
            {
                _logger.LogDebug($"User is NOT permitted for topic: {records.ToJsonString()}");
                return default;
            }
            _logger.LogDebug($"User is permitted for topic: {records.ToJsonString()}");

            return await _tokenGenerator.Next();
        }
    }
}
