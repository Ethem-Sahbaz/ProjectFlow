using ProjectFlow.Application.Shared;
using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectJoinRequestReader
{
    Task<Result<IReadOnlyList<ProjectJoinRequestResponse>>> GetProjectJoinRequestsAsync(Guid projectId, Guid userId);
}
