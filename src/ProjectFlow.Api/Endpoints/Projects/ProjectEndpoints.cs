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

        app.MapDelete(ApiEndpoints.Projects.Delete, async (IProjectDeleter deletor, HttpContext context, Guid id) =>
        {
            var userId = context.GetUserId();

            var deleteResult = await deletor.DeleteByIdAsync(id, userId);

            if (deleteResult.IsSuccess)
            {
                return Results.NoContent();
            }

            if (deleteResult.Error == ProjectMemberErrors.NotOwner)
            {
                return Results.Unauthorized();
            }

            return Results.NotFound(deleteResult.Error);
        })
        .RequireAuthorization()
        .WithName("DeleteProject")
        .WithOpenApi();

        app.MapPut(ApiEndpoints.Projects.Update, async (IProjectUpdater updater, UpdateProjectRequest request, HttpContext context, Guid id) =>
        {
            var userId = context.GetUserId();

            var updateResult = await updater.UpdateAsync(request, id, userId);

            if (updateResult.IsFailure)
                return Results.NotFound(updateResult.Error);

            return Results.Ok(updateResult.Value);

        })
        .WithName("UpdateProject")
        .RequireAuthorization()
        .AddEndpointFilter<ValidationFilter<UpdateProjectRequest>>()
        .WithOpenApi();

        app.MapGet(ApiEndpoints.Projects.GetProjectMembers, async (IProjectMemberReader reader, Guid id) =>
        {
            var membersResult = await reader.GetMembers(id);

            if (membersResult.IsFailure)
            {
                return Results.NotFound(membersResult.Error);
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

            if (joinRequestsResult.IsSuccess)
                return Results.Ok(joinRequestsResult.Value);

            if (joinRequestsResult.Error == ProjectMemberErrors.NotOwner)
                return Results.Unauthorized();

            return Results.NotFound(joinRequestsResult.Error);


        })
        .WithName("GetJoinRequests")
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
        .WithName("CreateJoinRequest")
        .RequireAuthorization()
        .WithOpenApi();

        app.MapPost(
            ApiEndpoints.Projects.PostJoinRequest,
            async (IProjectJoinRequestHandler handler, HandleProjectJoinRequest handleRequest, HttpContext context, Guid projectId, Guid requestId) =>
        {
            var userId = context.GetUserId();

            var handleResult = await handler.HandleRequestAsync(handleRequest, projectId, userId, requestId);

            if (handleResult.IsFailure)
            {
                return Results.BadRequest(handleResult.Error);
            }

            return Results.Ok();

        })
        .WithName("ProcessJoinRequest")
        .RequireAuthorization()
        .WithOpenApi();

        return app;
    }
}
