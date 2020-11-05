using Messagee.API.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messagee.API.Services.Topics
{
    public interface ITopicService
    {
        Task<object> GetRegistrationToken(IEnumerable<TopicPermissionRecord> registrations);
    }
}
