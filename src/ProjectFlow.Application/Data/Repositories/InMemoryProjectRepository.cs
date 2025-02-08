using ProjectFlow.Application.Domain.Projects;

namespace ProjectFlow.Application.Data.Repositories;
internal sealed class InMemoryProjectRepository : IProjectRepository
{
    private readonly List<Project> _projects = new();
    public Task<Project> AddAsync(Project project)
    {
        _projects.Add(project);

        return Task.FromResult(project);
    }

    public Task<IReadOnlyList<Project>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Project>>(_projects);
    }

    public Task<Project?> GetByIdAsync(Guid id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);

        return Task.FromResult(project);
    }

    public Task<IReadOnlyList<Project>> GetPublicProjectsAsync()
    {
        var publicProjects = _projects
            .Where(p => p.IsPublic)
            .ToList();

        return Task.FromResult<IReadOnlyList<Project>>(publicProjects);
    }
}
