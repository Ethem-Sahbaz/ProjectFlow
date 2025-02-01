namespace ProjectFlow.Application.Domain.Projects;
public interface IProjectRepository
{
    Task<IReadOnlyList<Project>> GetAllAsync();
    Task<IReadOnlyList<Project>> GetPublicProjectsAsync();
    Task<Project> AddAsync(Project project);
}
