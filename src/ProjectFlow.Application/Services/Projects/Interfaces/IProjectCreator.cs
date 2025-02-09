using ProjectFlow.Application.Shared;
using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectCreator
{
    Task<Result<ProjectResponse>> CreateAsync(Guid userId,CreateProjectRequest request);
}
