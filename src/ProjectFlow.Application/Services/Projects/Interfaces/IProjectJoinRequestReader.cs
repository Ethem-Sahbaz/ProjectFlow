using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectJoinRequestReader
{
    Task<IReadOnlyList<ProjectJoinRequestResponse>?> GetProjectJoinRequestsAsync(Guid projectId, Guid userId);
}
