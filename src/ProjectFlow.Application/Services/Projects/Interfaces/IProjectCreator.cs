using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectCreator
{
    Task<ProjectResponse> CreateAsync(Guid userId,CreateProjectRequest request);
}
