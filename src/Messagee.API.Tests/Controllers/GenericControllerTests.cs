using System;
using Xunit;
using Messagee.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Messagee.API.Tests.Controllers
{
    public class GenericControllerTests
    {
        [Theory]
        [InlineData(typeof(ConfigController), "config")]
        public void ValidateRoute(Type type, string expTemplate)
        {
            var route = type.GetCustomAttribute<RouteAttribute>();
            route.Template.ShouldBe(expTemplate);
        }
        [Theory]
        [InlineData(typeof(ConfigController), nameof(ConfigController.Post), "POST")]
        public void ValidateVerbs(Type type, string methodName, string expHttpVerb)
        {
            var mi = type.GetMethod(methodName);
            var att = mi.GetCustomAttribute<HttpMethodAttribute>();
            att.HttpMethods.First().ShouldBe(expHttpVerb);
        }

        [Theory]
        [InlineData(typeof(ConfigController), nameof(ConfigController.Post), "config-create")]
        public void ValidateAuthorizationRoles(Type type, string methodName, string expRoles)
        {
            var mi = type.GetMethod(methodName);
            var att = mi.GetCustomAttribute<AuthorizeAttribute>();
            att.Roles.ShouldBe(expRoles);
        }
    }
}
