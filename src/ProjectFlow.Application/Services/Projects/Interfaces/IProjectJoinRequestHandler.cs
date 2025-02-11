using ProjectFlow.Application.Shared;
using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectJoinRequestHandler
{
    Task<Result> HandleRequestAsync(
        HandleProjectJoinRequest handleRequest,
        Guid projectId,
        Guid userId,
        Guid joinRequestId);
}
