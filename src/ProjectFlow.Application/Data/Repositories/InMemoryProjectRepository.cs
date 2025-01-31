using ProjectFlow.Application.Domain.Projects;

namespace ProjectFlow.Application.Data.Repositories;
internal sealed class InMemoryProjectRepository : IProjectRepository
{
    private readonly List<Project> _projects = new();
    public InMemoryProjectRepository()
    {
        SeedData();
    }
    public Task<IReadOnlyList<Project>> GetAll()
    {
        return Task.FromResult<IReadOnlyList<Project>>(_projects);
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
