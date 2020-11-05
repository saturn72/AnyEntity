using Messagee.API.Domain;
using Messagee.API.Services.Security;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Messagee.API.Tests.Services.Topics
{
    public class PermissionManagerExtensionsTests
    {
        [Fact]
        public async Task PermittedForTopic_ByRecord_CallsPermittedForTopics()
        {
            var pm = new Mock<IPermissionManager>();
            var r = new TopicPermissionRecord();
            await PermissionManagerExtensions.PermittedForTopic(pm.Object, r);
            pm.Verify(p => p.PermittedForTopics(It.Is<IEnumerable<TopicPermissionRecord>>(e => e.Count() == 1 && e.ElementAt(0) == r)), Times.Once);
        }

        [Fact]
        public async Task PermittedForTopic_ByParams_CallsPermittedForTopics()
        {
            var pm = new Mock<IPermissionManager>();
            string a = "a",
                n = "n",
                t = "t",
                pt = "pt";

            await PermissionManagerExtensions.PermittedForTopic(pm.Object, a, n, t, pt);
            pm.Verify(p => p.PermittedForTopics(It.Is<IEnumerable<TopicPermissionRecord>>(
                e => e.Count() == 1 &&
                e.ElementAt(0).Account == a &&
                e.ElementAt(0).Namespace == n &&
                e.ElementAt(0).Topic == t &&
                e.ElementAt(0).PermissionType == pt)), Times.Once);
        }
    }
}
