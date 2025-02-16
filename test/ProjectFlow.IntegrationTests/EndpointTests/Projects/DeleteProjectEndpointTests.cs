using ProjectFlow.Api.Endpoints;
using ProjectFlow.Contracts.Identity;
using ProjectFlow.Contracts.Projects;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectFlow.IntegrationTests.EndpointTests.Projects;
public class DeleteProjectEndpointTests(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();


    [Fact]
    public async Task RequestWithoutTokenShouldReturnUnauthorized()
    {
        var result = await _client.DeleteAsync($"/api/projects/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }

    [Fact]
    public async Task ExistingProjectShouldBeDeleted()
    {
        var token = await GetTokenAsync();

        CreateProjectRequest createProjectRequest = new("Test Project", "No Description", true);

        _client.DefaultRequestHeaders.Authorization = new("Bearer", token);

        var result = await _client.PostAsJsonAsync(ApiEndpoints.Projects.Post, createProjectRequest);

        if (result.StatusCode != HttpStatusCode.Created)
            throw new Exception("Project could not be created.");

        var json = await result.Content.ReadAsStringAsync();

        var projectResponse = JsonSerializer.Deserialize<ProjectResponse>(
            json,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});

        var projectId = projectResponse!.Id;

        var deleteResult = await _client.DeleteAsync($"/api/projects/{projectId}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResult.StatusCode);
    }

    [Fact]
    public async Task NotExistingProjectShoulReturnNotFound()
    {
        var token = await GetTokenAsync();

        _client.DefaultRequestHeaders.Authorization = new("Bearer", token);

        var deleteResult = await _client.DeleteAsync($"/api/projects/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, deleteResult.StatusCode);

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
