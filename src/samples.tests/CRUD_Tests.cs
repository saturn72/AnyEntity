using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using sample;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace samples.tests
{
    public sealed class HttpMethods
    {
        public const string Get = "get";
        public const string Put = "put";
        public const string Patch = "patch";
        public const string Delete = "delete";
    }
    public class CRUD_Tests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly string _timeStamp;
        private const string Uri = "testclass1/";
        public CRUD_Tests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
            _timeStamp = DateTime.UtcNow.ToString("yyyy-mm-dd-hh:MM:ss.fff");
        }
        [Theory]
        [InlineData(HttpMethods.Get, "testclass1/filter?filterData")]
        [InlineData(HttpMethods.Put, "testclass1/123")]
        [InlineData(HttpMethods.Patch, "testclass1/123")]
        [InlineData(HttpMethods.Delete, "testclass1/123")]
        public async Task RedirectToControllerTests(string httpMethod, string uri)
        {
            var e1 = new
            {
                NumericValue = 1,
                StringValue = "1_init-value" + _timeStamp
            };
            var e2 = new
            {
                NumericValue = 2,
                StringValue = "2_init-value" + _timeStamp
            };
            var entities = new[] { e1, e2 };
            //Create
            var createRes = await _httpClient.PostAsJsonAsync(Uri, entities);
            createRes.EnsureSuccessStatusCode();
            var c = await createRes.Content.ReadAsStringAsync();
            var createEntities = JArray.Parse(c);
            createEntities.Count.ShouldBe(entities.Length);

            for (var i = 0; i < entities.Length; i++)
            {
                createEntities[i]["id"].Value<string>().ShouldNotBeNullOrEmpty();
                createEntities[i]["numericValue"].Value<int>().ShouldBe(entities[i].NumericValue);
                createEntities[i]["stringValue"].Value<string>().ShouldBe(entities[i].StringValue);
            }

            //GetById
            var id = createEntities[0]["id"].Value<string>();
            //GetById
            var getByIdRes = await _httpClient.GetAsync($"{Uri}{id}");
            await System.IO.File.WriteAllTextAsync(@"C:\Users\roi.shabtai\Desktop\1.html", await getByIdRes.Content.ReadAsStringAsync());
            getByIdRes.EnsureSuccessStatusCode();
            var g = await createRes.Content.ReadAsStringAsync();
            var getByIdEntity = JObject.Parse(g);
            createEntities["id"].Value<string>().ShouldBe(id);
            createEntities["numericValue"].Value<int>().ShouldBe(entities[0].NumericValue);
            createEntities["stringValue"].Value<string>().ShouldBe(entities[0].StringValue);
        }

        private Task<HttpResponseMessage> SubmitHttpRequest(string httpMethod, string uri, object content)
        {
            var sc = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            switch (httpMethod)
            {
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