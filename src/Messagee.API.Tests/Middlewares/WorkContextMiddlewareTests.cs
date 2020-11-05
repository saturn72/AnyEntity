using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Messagee.API.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace Messagee.API.Tests.Middlewares
{
    public class WorkContextMiddlewareTests
    {
        [Fact]
        public async Task ClientIdIsNull_ReturnUnauthroized()
        {
            var logger = new Mock<ILogger<WorkContextMiddleware>>();
            var wcm = new WorkContextMiddleware(null, logger.Object);

            var user = new Mock<ClaimsPrincipal>();
            user.Setup(u => u.Claims).Returns(new Claim[] { });
            var hc = new Mock<HttpContext>();
            var hr = new Mock<HttpResponse>();
            hc.Setup(h => h.User).Returns(user.Object);
            hc.Setup(h => h.Response).Returns(hr.Object);
            await wcm.InvokeAsync(hc.Object, null);
            hr.VerifySet(r => r.StatusCode = StatusCodes.Status401Unauthorized, Times.Once);
        }
        [Fact]
        public async Task UserIdExists_ClientIdIsNull_BuildsWorkContext()
        {
            string un = "user-name";
            var expRoles = new[] { "role1", "role2", "role3" };
            var logger = new Mock<ILogger<WorkContextMiddleware>>();
            var i = 0;
            RequestDelegate n = c =>
            {
                i++;
                return Task.CompletedTask;
            };
            var wcm = new WorkContextMiddleware(n, logger.Object);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, un),
                new Claim(ClaimTypes.Role, expRoles[0]),
                new Claim(ClaimTypes.Role, expRoles[1]),
                new Claim(ClaimTypes.Role, expRoles[2]),
            };
            var hc = new Mock<HttpContext>();
            hc.Setup(h => h.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(claims)));
            var wc = new WorkContext();
            await wcm.InvokeAsync(hc.Object, wc);

            wc.CurrentUserId.ShouldBe(un);
            wc.CurrentRoles.Count().ShouldBe(expRoles.Length);
            wc.CurrentRoles.ShouldAllBe(e => expRoles.Contains(e));
            i.ShouldBe(1);
        }
        [Fact]
        public async Task UserIdIsNull_ClientIdExists_BuildsWorkContext()
        {
            string ci = "user-name";
            var expRoles = new[] { "role1", "role2", "role3" };
            var logger = new Mock<ILogger<WorkContextMiddleware>>();
            var i = 0;
            RequestDelegate n = c =>
            {
                i++;
                return Task.CompletedTask;
            };
            var wcm = new WorkContextMiddleware(n, logger.Object);

            var claims = new[]
            {
                new Claim("client_id", ci),
                new Claim(ClaimTypes.Role, expRoles[0]),
                new Claim(ClaimTypes.Role, expRoles[1]),
                new Claim(ClaimTypes.Role, expRoles[2]),
            };
            var hc = new Mock<HttpContext>();
            hc.Setup(h => h.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(claims)));
            var wc = new WorkContext();
            await wcm.InvokeAsync(hc.Object, wc);

            wc.CurrentClientId.ShouldBe(ci);
            wc.CurrentRoles.Count().ShouldBe(expRoles.Length);
            wc.CurrentRoles.ShouldAllBe(e => expRoles.Contains(e));
            i.ShouldBe(1);
        }
    }
}