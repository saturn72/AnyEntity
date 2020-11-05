using Messagee.API.Domain;
using Messagee.API.Services.Topics;
using Messagee.API.Tests.Services.Topics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messagee.API.Services.Security
{
    public class PermissionManager : IPermissionManager
    {
        #region fields
        private readonly WorkContext _workContext;
        private readonly ITopicPermissionRecordRepository _repository;
        private readonly ILogger<PermissionManager> _logger;
        #endregion
        #region ctor
        public PermissionManager(
            WorkContext workContext,
            ITopicPermissionRecordRepository repository,
            ILogger<PermissionManager> logger)
        {
            _workContext = workContext;
            _repository = repository;
            _logger = logger;
        }
        #endregion

        public async Task<bool> PermittedForTopics(IEnumerable<TopicPermissionRecord> records)
        {
            _logger.LogInformation($"Start {nameof(PermittedForTopics)} with params: {nameof(records)} = {records.ToJsonString()}");
            if (!ValidateRecords(records)) return false;
            var entries = await _repository.GetUserTopicPermissionRecords(_workContext.CurrentUserId, _workContext.CurrentClientId, records);

            return AreCollectionssOverlaped(records, entries);
        }

        private static bool AreCollectionssOverlaped(IEnumerable<TopicPermissionRecord> col1, IEnumerable<TopicPermissionRecord> col2)
        {
            return
                !col1.IsNullOrEmpty() &&
                !col2.IsNullOrEmpty() &&
                col1.Count() == col2.Count() &&
                col1.All(r => col2.Any(x =>
                    x.Account == r.Account &&
                    x.Namespace == r.Namespace &&
                    x.Topic == r.Topic &&
                    x.PermissionType == r.PermissionType));
        }

        private static bool ValidateRecords(IEnumerable<TopicPermissionRecord> records)
        {
            return
                !records.IsNullOrEmpty() &&
                records.All(r =>
                    r.Account.HasValue() &&
                    r.Namespace.HasValue() &&
                    r.Topic.HasValue() &&
                    r.PermissionType.HasValue() &&
                    TopicPermissionTypes.All.Contains(r.PermissionType));
        }
    }
}
