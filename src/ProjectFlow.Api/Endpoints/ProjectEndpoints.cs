using ProjectFlow.Application.Services.Projects.Interfaces;
using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Api.Endpoints;

internal static class ProjectEndpoints
{
    internal static WebApplication UseProjectEndpoints(this WebApplication app)
    {
        app.MapGet(ApiEndpoints.Projects.Get, async (IProjectsReader reader) =>
        {
            var projects = await reader.GetAllProjectsAsync();

            return Results.Ok(projects);
        })
        .WithName("GetAllProjects")
        .WithOpenApi();

        app.MapPost(ApiEndpoints.Projects.Post, async (IProjectCreator creator, CreateProjectRequest request) =>
        {
            var response = await creator.CreateAsync(request);

            return Results.Created($"{ApiEndpoints.Projects.Post}/{response.Id}", response);
        })
        .WithName("CreateProject")
        .WithOpenApi();

        app.MapGet(ApiEndpoints.Projects.GetProjectMembers, async (IProjectMemberReader reader, Guid id) =>
        {
            var members = await reader.GetMembers(id);

            return Results.Ok(members);
        })
        .WithName("GetProjectMembers")
        .RequireAuthorization()
        .WithOpenApi();

        return app;
    }
}
