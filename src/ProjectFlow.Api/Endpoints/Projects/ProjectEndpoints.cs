using ProjectFlow.Api.Authentication;
using ProjectFlow.Api.Filters;
using ProjectFlow.Application.Services.Projects.Interfaces;
using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Api.Endpoints.Projects;

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

        app.MapPost(ApiEndpoints.Projects.Post, async (IProjectCreator creator, HttpContext context, CreateProjectRequest request) =>
        {
            var userId = context.GetUserId();

            var response = await creator.CreateAsync(userId, request);

            return Results.Created($"{ApiEndpoints.Projects.Post}/{response.Id}", response);
        })
        .WithName("CreateProject")
        .AddEndpointFilter<ValidationFilter<CreateProjectRequest>>()
        .RequireAuthorization()
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
