using FluentValidation.TestHelper;
using Messagee.API.Models;
using Messagee.API.Services.Topics;
using Messagee.API.Validators;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Messagee.API.Tests.Validators
{
    public class RegistrationModelValidatorTests
    {
        [Fact]
        public async Task TopicRegistration_Rules()
        {
            var v = new RegistrationModelValidator();
            var cd = new RegistrationModel();
            var res = await v.TestValidateAsync(cd);
            res.ShouldHaveValidationErrorFor(c => c.Topics);
        }
        public class TestRegistrationModelValidator : RegistrationModelValidator
        {
            [Theory]
            [MemberData(nameof(TopicREgistrationInternal_Rules_DATA))]
            public void TopicRegistrationInternal_Rules(TopicRegistrationRequest r)
            {
                VerifyTopicRegistrationRequest(r).ShouldBeFalse();
            }
            public static IEnumerable<object[]> TopicREgistrationInternal_Rules_DATA = new[]
            {
            new object[]{ new TopicRegistrationRequest() },
            new object[]{ new TopicRegistrationRequest { Account = "acc" }},
            new object[]{ new TopicRegistrationRequest { Account = "acc", Namespace = "ns" }},
            new object[]{ new TopicRegistrationRequest { Account = "acc", Namespace = "ns", Topic="t" }},
             new object[]{ new TopicRegistrationRequest { Account = "acc", Namespace = "ns", Topic="t" , ReferenceId="rid"}
             },
             new object[]{ new TopicRegistrationRequest { Account = "acc", Namespace = "ns", Topic="t" , ReferenceId="rid", PermissionType = "not-exists" } },
            };
            [Fact]
            public void TopicRegistrationInternal_Rules_ReturnsTrue()
            {
                var r = new TopicRegistrationRequest
                {
                    Account = "acc",
                    Namespace = "ns",
                    Topic = "t",
                    ReferenceId = "rid",
                    PermissionType = TopicPermissionTypes.Publisher
                };
                VerifyTopicRegistrationRequest(r).ShouldBeTrue();
            }
        }
    }
}
