using ProjectFlow.Application.Shared;
using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectUpdater
{
    Task<Result<ProjectResponse>> UpdateAsync(UpdateProjectRequest request, Guid projectId, Guid userId);
}
