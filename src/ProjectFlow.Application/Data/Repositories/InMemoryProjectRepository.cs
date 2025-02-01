using ProjectFlow.Application.Domain.Projects;

namespace ProjectFlow.Application.Data.Repositories;
internal sealed class InMemoryProjectRepository : IProjectRepository
{
    private readonly List<Project> _projects = new();
    public InMemoryProjectRepository()
    {
        SeedData();
    }

    public Task<Project> AddAsync(Project project)
    {
        _projects.Add(project);

        return Task.FromResult(project);
    }

    public Task<IReadOnlyList<Project>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Project>>(_projects);
    }

    public Task<IReadOnlyList<Project>> GetPublicProjectsAsync()
    {
        var publicProjects = _projects
            .Where(p => p.IsPublic)
            .ToList();

        return Task.FromResult<IReadOnlyList<Project>>(publicProjects);
    }

    private void SeedData()
    {
        _projects.AddRange(new List<Project>
        {
            new Project(Guid.NewGuid(), "Project Alpha", "Description for Project Alpha", true),
            new Project(Guid.NewGuid(), "Project Beta", "Description for Project Beta", false),
            new Project(Guid.NewGuid(), "Project Gamma", "Description for Project Gamma", true)
        });
    }
}
