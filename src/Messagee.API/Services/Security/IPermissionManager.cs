using Messagee.API.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messagee.API.Services.Security
{
    public interface IPermissionManager
    {
        Task<bool> PermittedForTopics(IEnumerable<TopicPermissionRecord> records);
    }
}
