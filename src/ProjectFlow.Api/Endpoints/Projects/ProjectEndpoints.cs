using ProjectFlow.Api.Authentication;
using ProjectFlow.Api.Filters;
using ProjectFlow.Application.Domain.ProjectMembers.Errors;
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

            var createResult = await creator.CreateAsync(userId, request);

            if (createResult.IsFailure)
            {
                return Results.BadRequest(createResult.Error);
            }

            var projectResponse = createResult.Value;

            return Results.Created($"{ApiEndpoints.Projects.Post}/{projectResponse.Id}", projectResponse);
        })
        .WithName("CreateProject")
        .AddEndpointFilter<ValidationFilter<CreateProjectRequest>>()
        .RequireAuthorization()
        .WithOpenApi();

        app.MapDelete(ApiEndpoints.Projects.Delete, async (IProjectDeletor deletor, HttpContext context, Guid id) =>
        {
            var userId = context.GetUserId();

            var deleteResult = await deletor.DeleteByIdAsync(id, userId);

            if (deleteResult.IsFailure)
            {
                if (deleteResult.Error == ProjectMemberErrors.NotOwner)
                {
                    return Results.Unauthorized();
                }

                return Results.NotFound(deleteResult.Error);
            }

            return Results.NoContent();
        })
        .RequireAuthorization()
        .WithName("DeleteProject")
        .WithOpenApi();

        app.MapGet(ApiEndpoints.Projects.GetProjectMembers, async (IProjectMemberReader reader, Guid id) =>
        {
            var membersResult = await reader.GetMembers(id);

            if (membersResult.IsFailure)
            {
                return Results.BadRequest(membersResult.Error);
            }

            return Results.Ok(membersResult.Value);
        })
        .WithName("GetProjectMembers")
        .RequireAuthorization()
        .WithOpenApi();

        app.MapGet(ApiEndpoints.Projects.GetJoinRequests, async (IProjectJoinRequestReader requestReader, HttpContext context, Guid id) =>
        {
            var userId = context.GetUserId();

            var joinRequestsResult = await requestReader.GetProjectJoinRequestsAsync(id, userId);

            if (joinRequestsResult.IsFailure)
            {
                if (joinRequestsResult.Error == ProjectMemberErrors.NotOwner)
                {
                    return Results.Unauthorized();
                }

                return Results.NotFound(joinRequestsResult.Error);
            }

            return Results.Ok(joinRequestsResult.Value);
        })
        .RequireAuthorization()
        .WithOpenApi();

        app.MapPost(ApiEndpoints.Projects.GetJoinRequests, async (IProjectJoinRequestCreator creator, CreateProjectJoinRequest request, HttpContext context, Guid id) =>
        {
            var userId = context.GetUserId();

            var createResult = await creator.CreateJoinRequestAsync(userId, id, request);

            if (createResult.IsFailure)
            {
                return Results.NotFound(createResult.Error);
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
