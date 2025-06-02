using System.Net;
using System.Text;
using System.Text.Json;
using FastStats.Api.Features.Computations.Models;
using FastStats.Domain.ValueObjects;

namespace FastStats.Api.Tests.IntegrationTests;

public sealed class ComputationsTests : IClassFixture<FastStatsApiFactory>
{
    private readonly HttpClient _client;

    public ComputationsTests(FastStatsApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData(ComputationStrategies.Naive)]
    [InlineData(ComputationStrategies.Parallel)]
    public async Task StatisticalCompute_ReturnsCreated(ComputationStrategies expectedStrategy)
    {
        // Arrange
        var requestBody = BuildStatisticalComputeRequestBody([1m, 2m, 3m, 4m, 5m], expectedStrategy);

        // Act
        var response = await _client.PostAsync("/api/v1/computations", requestBody);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        var computationResult = DeserializeComputationResult(responseContent);
        Assert.NotNull(computationResult);
        Assert.NotEqual(Guid.Empty, computationResult.ComputationId);
        Assert.NotEqual(Guid.Empty, computationResult.DatasetId);
        Assert.Equal(
            expectedStrategy is ComputationStrategies.Naive
                ? ComputationStrategyIdentifier.Naive.Name
                : ComputationStrategyIdentifier.Parallel.Name, computationResult.Strategy);
    }

    [Fact]
    public async Task GetComputationById_ReturnsOk()
    {
        // Arrange
        var requestBody = BuildStatisticalComputeRequestBody([1m, 2m, 3m, 4m, 5m], ComputationStrategies.Naive);
        var creationResponse = await _client.PostAsync("/api/v1/computations", requestBody);
        var creationResponseContent = await creationResponse.Content.ReadAsStringAsync();
        var createdComputationResult = DeserializeComputationResult(creationResponseContent);

        // Act
        var response =
            await _client.GetAsync($"/api/v1/computations/{createdComputationResult?.ComputationId ?? Guid.Empty}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        var computationResult = DeserializeComputationResult(responseContent);
        Assert.NotNull(computationResult);
        Assert.Equivalent(createdComputationResult, computationResult);
    }

    private static StringContent BuildStatisticalComputeRequestBody(
        IReadOnlyList<decimal> data,
        ComputationStrategies strategy)
    {
        var request = new ComputationRequest(data, strategy);
        var jsonRequest = JsonSerializer.Serialize(request);
        return new StringContent(jsonRequest, Encoding.UTF8, "application/json");
    }

    private static ComputationResult? DeserializeComputationResult(string jsonContent) =>
        JsonSerializer.Deserialize<ComputationResult>(jsonContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
}