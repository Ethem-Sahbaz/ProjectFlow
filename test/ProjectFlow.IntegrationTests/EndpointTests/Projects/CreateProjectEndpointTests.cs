using ProjectFlow.Api.Endpoints;
using ProjectFlow.Contracts.Identity;
using ProjectFlow.Contracts.Projects;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectFlow.IntegrationTests.EndpointTests.Projects;
public class CreateProjectEndpointTests(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task ShouldReturnUnauthorizedWithoutToken()
    {
        var createProjectRequest = new CreateProjectRequest("Test Project", "No Description", true);

        var result = await _client.PostAsJsonAsync(ApiEndpoints.Projects.Post, createProjectRequest);

        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }

    [Fact]
    public async Task RequestWithTokenAndValidBodyShouldCreateProject()
    {
        var token = await GetTokenAsync();

        CreateProjectRequest createProjectRequest = new("Test Project", "No Description", true);

        _client.DefaultRequestHeaders.Authorization = new("Bearer", token);

        var result = await _client.PostAsJsonAsync(ApiEndpoints.Projects.Post, createProjectRequest);

        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }

    [Fact]
    public async Task RequestWithTokenAndEmptyProjectNameShouldReturnBadRequest()
    {
        var token = await GetTokenAsync();

        CreateProjectRequest createProjectRequest = new("", "No Description", true);

        _client.DefaultRequestHeaders.Authorization = new("Bearer", token);

        var result = await _client.PostAsJsonAsync(ApiEndpoints.Projects.Post, createProjectRequest);

        var content = await result.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task RequestWithTokenAndInvalidJsonShouldReturnBadRequest()
    {
        var token = await GetTokenAsync();

        _client.DefaultRequestHeaders.Authorization = new("Bearer", token);

        var result = await _client.PostAsJsonAsync(ApiEndpoints.Projects.Post, "{{{adasd}");

        var content = await result.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    private async Task<string> GetTokenAsync()
    {
        var tokenGenerationRequest = new TokenGenerationRequest()
        {
            UserId = Guid.Parse("5bc38e06-2deb-459e-8bb8-299daa4e3e20"),
            Email = "test@test.de"
        };

        var response = await _client.PostAsJsonAsync(ApiEndpoints.Identity.Token, tokenGenerationRequest);

        var token = await response.Content.ReadAsStringAsync();

        // Removes double quotes
        token = token.Replace("\"", string.Empty);

        return token;

    }
}
