using Messagee.API.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messagee.API.Tests.Services.Topics
{
    public interface ITopicPermissionRecordRepository
    {
        Task<IEnumerable<TopicPermissionRecord>> GetUserTopicPermissionRecords(string currentUserId, string currentClientId, IEnumerable<TopicPermissionRecord> records);
    }
}
