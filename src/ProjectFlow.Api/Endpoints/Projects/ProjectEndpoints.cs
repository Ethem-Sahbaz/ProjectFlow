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

            if (members is null)
            {
                return Results.NotFound("Project not found.");
            }

            return Results.Ok(members);
        })
        .WithName("GetProjectMembers")
        .RequireAuthorization()
        .WithOpenApi();

        app.MapGet(ApiEndpoints.Projects.GetJoinRequests, async (IProjectJoinRequestReader requestReader, HttpContext context, Guid id) =>
        {
            var userId = context.GetUserId();

            var joinRequests = await requestReader.GetProjectJoinRequestsAsync(id, userId);

            if (joinRequests is null)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(joinRequests);
        })
        .RequireAuthorization()
        .WithOpenApi();

        app.MapPost(ApiEndpoints.Projects.GetJoinRequests, async (IProjectJoinRequestCreator creator, CreateProjectJoinRequest request, HttpContext context, Guid id) =>
        {
            var userId = context.GetUserId();

            var created = await creator.CreateJoinRequestAsync(userId, id, request);

            if (!created)
            {
                return Results.NotFound();
            }

            return Results.Created();

        })
        .RequireAuthorization()
        .WithOpenApi();

        app.MapPost(ApiEndpoints.Projects.PostJoinRequest, () =>
        {

        })
        .RequireAuthorization()
        .WithOpenApi();

        return app;
    }
}
