using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using sample;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using System;

namespace samples.tests
{
    public sealed class HttpMethods
    {
        public const string Post = "post";
        public const string Get = "get";
        public const string Put = "put";
        public const string Patch = "patch";
        public const string Delete = "delete";
    }
    public class CRUD_Tests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly string _timeStamp;

        public CRUD_Tests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
            _timeStamp = DateTime.UtcNow.ToString("yyyy-mm-dd-hh:MM:ss.fff");
        }
        [Theory]
        [InlineData(HttpMethods.Post, "testclass1")]
        [InlineData(HttpMethods.Get, "testclass1/123")]
        [InlineData(HttpMethods.Get, "testclass1/filter?filterData")]
        [InlineData(HttpMethods.Put, "testclass1/123")]
        [InlineData(HttpMethods.Patch, "testclass1/123")]
        [InlineData(HttpMethods.Delete, "testclass1/123")]
        public async Task RedirectToControllerTests(string httpMethod, string uri)
        {
            var content = new
            {
                NumericValue = 1,
                StringValue = "test_" + _timeStamp,
            };
            var res = await SubmitHttpRequest(httpMethod, uri, content);
            res.EnsureSuccessStatusCode();
            var msg = await res.Content.ReadAsStringAsync();
            msg.ShouldContain(httpMethod);
        }

        private Task<HttpResponseMessage> SubmitHttpRequest(string httpMethod, string uri, object content)
        {
            var sc = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            switch (httpMethod)
            {
                case HttpMethods.Post:
                    return _httpClient.PostAsync(uri, sc);
                case HttpMethods.Get:
                    return _httpClient.GetAsync(uri);
                case HttpMethods.Put:
                    return _httpClient.PutAsync(uri, sc);
                case HttpMethods.Patch:
                    return _httpClient.PatchAsync(uri, sc);
                case HttpMethods.Delete:
                    return _httpClient.DeleteAsync(uri);
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(httpMethod));
            }

        }
    }
}