using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using sample;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
    public class RouterTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        public RouterTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }
        [Theory]
        [InlineData(HttpMethods.Post, "test")]
        [InlineData(HttpMethods.Get, "test/123")]
        [InlineData(HttpMethods.Get, "test/filter/filterData")]
        [InlineData(HttpMethods.Put, "test/123")]
        [InlineData(HttpMethods.Patch, "test/123")]
        [InlineData(HttpMethods.Delete, "test/123")]
        public async Task RedirectToControllerTests(string httpMethod, string uri)
        {
            var content = new
            {
                Name = "test",
                Id = "123"
            };
            var res = await SubmitHttpRequest(httpMethod, uri, content);
            res.EnsureSuccessStatusCode();
        }

        private Task<HttpResponseMessage> SubmitHttpRequest(string httpMethod, string uri, object content)
        {
            switch (httpMethod)
            {
                case HttpMethods.Post:
                    return _httpClient.PostAsJsonAsync(uri, content);
                case HttpMethods.Get:
                    return _httpClient.GetAsync(uri);
                case HttpMethods.Put:
                    return _httpClient.PutAsJsonAsync(uri, content);
                case HttpMethods.Patch:
                    return _httpClient.PatchAsync(uri, new StringContent(JsonConvert.SerializeObject(content)));
                case HttpMethods.Delete:
                    return _httpClient.DeleteAsync(uri);
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(httpMethod));
            }

        }
    }
}