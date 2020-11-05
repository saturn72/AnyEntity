using Messagee.API.Security;
using Shouldly;
using Xunit;

namespace Messagee.API.Tests.Security
{
    public class MessageeClaimTypesTests
    {
        [Fact]
        public void AllClaims()
        {
            MessageeClaimTypes.Namespace.ShouldBe("namespace");
        }
    }
}
