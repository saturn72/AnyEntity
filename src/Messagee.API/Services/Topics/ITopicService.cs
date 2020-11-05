using Messagee.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messagee.API.Services.Topics
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicRegistration>> GetTopicIdsByTopicNames(string @namespace, IEnumerable<TopicRegistration> registrations);
    }
}
