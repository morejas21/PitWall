using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace PitWall.Tests.Api;

public class OpenApiTests
{
    [Theory]
    [InlineData("/openapi/v1.json")]
    [InlineData("/swagger/index.html")]
    public async Task IsServedInDevelopment(string path)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(host => host.UseEnvironment(Environments.Development)).CreateClient();

        var response = await client.GetAsync(new Uri(path, UriKind.Relative), TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("/openapi/v1.json")]
    [InlineData("/swagger/index.html")]
    public async Task IsNotServedInProduction(string path)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(host => host.UseEnvironment(Environments.Production)).CreateClient();

        var response = await client.GetAsync(new Uri(path, UriKind.Relative), TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
    }
}
