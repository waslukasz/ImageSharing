using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests;

public class AuthTest
{
    private HttpClient _client;
    
    public AuthTest()
    {
        WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ShouldReturnToken()
    {
        HttpContent content = JsonContent.Create(new { username = "admin", password = "Test123#" });
        HttpResponseMessage response = await _client.PostAsync("/api/auth/Login",content);
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();
        Assert.NotEqual(-1,responseString.IndexOf("token"));
    }

    [Fact]
    public async Task ShouldNotReturnToken()
    {
        HttpContent content = JsonContent.Create(new { username = "admin", password = "Test123" });
        HttpResponseMessage response = await _client.PostAsync("/api/auth/Login",content);
        Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);

        string responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal(-1,responseString.IndexOf("token"));
    }
}