using ProjectFlow.Api.Endpoints;
using ProjectFlow.Contracts.Identity;
using ProjectFlow.Contracts.Projects;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectFlow.IntegrationTests.EndpointTests.Projects;
public class UpdateProjectEndpointTests(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task RequestWithoutTokenReturnsUnauthorized()
    {
        var updateRequest = new UpdateProjectRequest("Testproject", "No Description", true);

        var response = await _client.PutAsJsonAsync($"/api/projects/{Guid.NewGuid()}", updateRequest);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task ExistingProjectShouldBeUpdated()
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
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var projectId = projectResponse!.Id;

        var updateProjectRequest = new UpdateProjectRequest("Testproject Updated", "No Description", true);

        var updateResult = await _client.PutAsJsonAsync($"/api/projects/{projectId}", updateProjectRequest);

        var updateJson = await updateResult.Content.ReadAsStringAsync();

        var updateResponse = JsonSerializer.Deserialize<ProjectResponse>(
            json,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        if(updateResponse is null)
            throw new Exception("Project could not be updated.");

        Assert.Equal(HttpStatusCode.OK, updateResult.StatusCode);
        Assert.Equal(createProjectRequest.Name, updateResponse.Name);
    }

    [Fact]
    public async Task ExistingProjectShouldNotUpdatedWhenProjectNameIsEmpty()
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
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        var projectId = projectResponse!.Id;

        var updateProjectRequest = new UpdateProjectRequest("", "No Description", true);

        var updateResult = await _client.PutAsJsonAsync($"/api/projects/{projectId}", updateProjectRequest);

        Assert.Equal(HttpStatusCode.BadRequest, updateResult.StatusCode);
    }

    [Fact]
    public async Task NotExistingProjectShoulReturnNotFound()
    {
        var token = await GetTokenAsync();

        _client.DefaultRequestHeaders.Authorization = new("Bearer", token);

        var updateProjectRequest = new UpdateProjectRequest("Testproject", "No Description", true);

        var updateResult = await _client.PutAsJsonAsync($"/api/projects/{Guid.NewGuid()}", updateProjectRequest);

        Assert.Equal(HttpStatusCode.NotFound, updateResult.StatusCode);
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
