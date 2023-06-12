using System.Net;
using System.Net.Http.Json;
using Infrastructure.EF.Pagination;
using Microsoft.AspNetCore.Mvc.Testing;
using WebAPI.Response;

namespace Tests;

public class PostTest
{
    private HttpClient _client;
    
    public PostTest()
    {
        WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAlbumTest()
    {
        string response = await _client.GetStringAsync("/api/Album/All");
        Assert.NotEqual(-1,response.IndexOf("\"totalItems\":6"));
        Assert.NotEqual(-1,response.IndexOf("\"itemsOnPage\":5"));
        Assert.NotEqual(-1,response.IndexOf("\"currentPage\":1"));
        
    }

    [Fact]
    public async Task GetAlbumTestPageTwo()
    {
        string response = await _client.GetStringAsync("/api/Album/All?Page=2");
        Assert.NotEqual(-1,response.IndexOf("\"totalItems\":6"));
        Assert.NotEqual(-1,response.IndexOf("\"itemsOnPage\":1"));
        Assert.NotEqual(-1,response.IndexOf("\"currentPage\":2"));
    }

}