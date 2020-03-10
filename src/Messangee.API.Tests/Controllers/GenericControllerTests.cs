using System;
using Xunit;
using Messangee.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using System.Linq;

namespace Messangee.API.Tests.Controllers
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
        [InlineData(typeof(ConfigController), "Get", "GET")]
        [InlineData(typeof(ConfigController), "Post", "POST")]
        public void ValidateVerbs(Type type, string methodName, string expHttpVerb)
        {
            var mi = type.GetMethod(methodName);
            var att = mi.GetCustomAttribute<HttpMethodAttribute>();
            att.HttpMethods.First().ShouldBe(expHttpVerb);
        }
    }
}
