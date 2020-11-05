using Messagee.API.Domain;
using System.Threading.Tasks;

namespace Messagee.API.Services.Security
{
    public static class PermissionManagerExtensions
    {
        public static Task<bool> PermittedForTopic(this IPermissionManager permissionManager, TopicPermissionRecord record)
        {
            return permissionManager.PermittedForTopics(
                new[] { record });
        }
        public static Task<bool> PermittedForTopic(this IPermissionManager permissionManager, string account, string @namespace, string topic, string permissionType)
        {
            return permissionManager.PermittedForTopics(
                new[] {
                    new TopicPermissionRecord {
                        Account = account,
                        Namespace = @namespace,
                        Topic = topic,
                        PermissionType = permissionType }
                });
        }
    }
}
