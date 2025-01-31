using ProjectFlow.Application.Domain.Projects;
using ProjectFlow.Application.Services.Projects.Interfaces;

namespace ProjectFlow.Application.Services.Projects;
internal sealed class ProjectService : IProjectsReader
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IReadOnlyList<Project>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAll();
    }
}
