using System;
using Xunit;

namespace Messagee.API.Tests.IntegrationTests
{
    public class RegistrationIntegrationTests
    {
        [Fact]
        public void UserNotAuthorizedForConfig()
        {
            throw new NotImplementedException("request POST on sonfigData controller while user has no config-create role");
        }
    }
}
