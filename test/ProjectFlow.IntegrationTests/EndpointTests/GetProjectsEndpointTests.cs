using ProjectFlow.Api.Endpoints;
using System.Net;

namespace ProjectFlow.IntegrationTests.EndpointTests;

public class GetProjectsEndpointTests(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task ShouldReturnSuccessCode()
    {
        var response = await _client.GetAsync(ApiEndpoints.Projects.Get);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }

}