using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Time.Testing;

namespace PitWall.Tests.Api;

public class TimeEndpointTests
{
    [Fact]
    public async Task GetTimeReturnsServerUtcTime()
    {
        var now = new DateTimeOffset(2026, 11, 22, 4, 0, 0, TimeSpan.Zero);
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(host => host.ConfigureTestServices(services =>
        {
            services.RemoveAll<TimeProvider>();
            services.AddSingleton<TimeProvider>(new FakeTimeProvider(now));
        })).CreateClient();

        var response = await client.GetAsync(new Uri("/api/time", UriKind.Relative), TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<JsonElement>(TestContext.Current.CancellationToken);
        Assert.Equal("2026-11-22T04:00:00+00:00", body.GetProperty("utcNow").GetString());
    }

    [Fact]
    public void SystemTimeProviderIsRegistered()
    {
        using var factory = new WebApplicationFactory<Program>();

        var timeProvider = factory.Services.GetRequiredService<TimeProvider>();

        Assert.Same(TimeProvider.System, timeProvider);
    }
}
