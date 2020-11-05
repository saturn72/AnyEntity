using Messagee.API.Domain;
using Messagee.API.Services.Security;
using Messagee.API.Services.Topics;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Messagee.API.Tests.Services.Topics
{
    public class PermissionManagerTests
    {
        [Theory]
        [MemberData(nameof(PermissionManager_RecordsNotOverlapped_DATA))]
        public async Task PermissionManager_RecordsNotOverlapped(IEnumerable<TopicPermissionRecord> entries)
        {
            var repo = new Mock<ITopicPermissionRecordRepository>();
            repo.Setup(r => r.GetUserTopicPermissionRecords(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<IEnumerable<TopicPermissionRecord>>()))
                .ReturnsAsync(entries);
            var logger = new Mock<ILogger<PermissionManager>>();

            var wc = new WorkContext
            {
                CurrentUserId = "uid",
                CurrentClientId = "cid",
            };
            var pm = new PermissionManager(wc, repo.Object, logger.Object);
            var res = await pm.PermittedForTopics(new[]{new TopicPermissionRecord
                {
                    Account = "acc",
                    Namespace = "ns",
                    Topic="t",
                    PermissionType = TopicPermissionTypes.Publisher
                }
            });
            res.ShouldBeFalse();
        }
        public static IEnumerable<object[]> PermissionManager_RecordsNotOverlapped_DATA = new[]
        {
            new object[]{ null},
            new object[]{ new []{new TopicPermissionRecord {
                UserId = "uid",
                ClientId = "cid",
                Account = "a1",
                Namespace = "n1",
                Topic="t1",
                PermissionType = TopicPermissionTypes.Publisher
            }
            }
            },
        };

        [Fact]
        public async Task PermissionManager_InvalidParams_NullCollection()
        {
            var logger = new Mock<ILogger<PermissionManager>>();
            var pm = new PermissionManager(null, null, logger.Object);
            var res = await pm.PermittedForTopics(null);
            res.ShouldBeFalse();
        }

        [Theory]
        [MemberData(nameof(PermissionManager_InvalidParams_DATA))]
        public async Task PermissionManager_InvalidParams(TopicPermissionRecord r)
        {
            var logger = new Mock<ILogger<PermissionManager>>();
            var pm = new PermissionManager(null, null, logger.Object);
            var res = await pm.PermittedForTopics(new[] { r });
            res.ShouldBeFalse();
        }
        public static IEnumerable<Object[]> PermissionManager_InvalidParams_DATA = new[]
        {
            new object[]
            {
                new TopicPermissionRecord
                {
                    Account = "acc"
                }
            },
            new object[]
            {
                new TopicPermissionRecord
                {
                    Account = "acc",
                    Namespace = "ns"
                }
            },
            new object[]
            {
                new TopicPermissionRecord
                {
                    Account = "acc",
                    Namespace = "ns",
                    Topic="t"
                }
            },
            new object[]
            {
                new TopicPermissionRecord
                {
                    Account = "acc",
                    Namespace = "ns",
                    Topic="t"
                }
            },
            new object[]
            {
                new TopicPermissionRecord
                {
                    Account = "acc",
                    Namespace = "ns",
                    Topic="t",
                    PermissionType = "not-exists"
                }
            },
        };
    }
}
