using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class CommentTest
    {
        private HttpClient _client;

        public CommentTest()
        {
            WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldReturnNotFound()
        {
            var response = await _client.GetAsync("/api/Comment/Get/1");
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnUnAuthorized()
        {
            var response = await _client.GetAsync("/api/Comment/Add");
            Assert.NotEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
