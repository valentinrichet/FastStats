using System.Text.Json.Serialization;

namespace FastStats.Api.Features.Computations.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ComputationStrategies
{
    Naive,
    Parallel
}