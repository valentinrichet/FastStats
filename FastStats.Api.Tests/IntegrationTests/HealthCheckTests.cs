using System.Net;

namespace FastStats.Api.Tests.IntegrationTests;

public sealed class HealthCheckTests : IClassFixture<FastStatsApiFactory>
{
    private readonly HttpClient _client;

    public HealthCheckTests(FastStatsApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetHealthCheck_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/health");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}