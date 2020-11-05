using Messagee.API.Models;
using Messagee.API.Services.Security;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly IDistributedCache _cache;
        private readonly ILogger<TopicService> _logger;
        #endregion

        #region ctor
        public TopicService(
            IPermissionManager permissionManager,
            IDistributedCache cache,
            ILogger<TopicService> logger)
        {
            _permissionManager = permissionManager;
            _cache = cache;
            _logger = logger;
        }
        #endregion

        public async Task<IEnumerable<TopicRegistration>> GetTopicIdsByTopicNames(string @namespace, IEnumerable<TopicRegistration> registrations)
        {
            _logger.LogInformation($"Start {nameof(GetTopicIdsByTopicNames)} with params: {nameof(@namespace)} = {@namespace}, {nameof(registrations)} = {registrations.ToJsonString()}");
            if (!await _permissionManager.UserPermittedForNamespace(@namespace))
            {
                _logger.LogDebug($"User is not permitted for namespace");
                return null;
            }

            var all = await _cache.GetAsync
            throw new System.NotImplementedException();
        }
    }
}
