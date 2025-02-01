using ProjectFlow.Application.Domain.Projects;
using ProjectFlow.Application.Mapping;
using ProjectFlow.Application.Services.Projects.Interfaces;
using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects;
internal sealed class ProjectService : IProjectsReader, IProjectCreator
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request)
    {
        Project project = new(Guid.NewGuid(), request.Name, request.Description, request.isPublic);

        await _projectRepository.AddAsync(project);

        return project.MapToResponse();
    }

    public async Task<IReadOnlyList<ProjectResponse>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();

        return projects
            .Select(p => p.MapToResponse())
            .ToList();
    }
}
