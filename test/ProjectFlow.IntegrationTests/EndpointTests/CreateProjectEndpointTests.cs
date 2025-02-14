using ProjectFlow.Api.Endpoints;
using ProjectFlow.Contracts.Identity;
using ProjectFlow.Contracts.Projects;
using System.Net;
using System.Net.Http.Json;

namespace ProjectFlow.IntegrationTests.EndpointTests;
public class CreateProjectEndpointTests(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task ShouldReturnUnauthorizedWithoutToken()
    {
        var createProjectRequest = new CreateProjectRequest("Test Project", "No Description", true);

        var result = await _client.PostAsJsonAsync(ApiEndpoints.Projects.Post, createProjectRequest);

        Assert.Equal(HttpStatusCode.Unauthorized,result.StatusCode);
    }

    private async Task<string> GetToken()
    {
        var tokenGenerationRequest = new TokenGenerationRequest()
        {
            UserId = Guid.Parse("5bc38e06-2deb-459e-8bb8-299daa4e3e20"),
            Email = "test@test.de"
        };

        var response = await _client.PostAsJsonAsync(ApiEndpoints.Identity.Token, tokenGenerationRequest);

        var token = await response.Content.ReadAsStringAsync();

        return token;

    }
}
