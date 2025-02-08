namespace ProjectFlow.Application.Domain.Projects;
public interface IProjectRepository
{
    Task<IReadOnlyList<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Project>> GetPublicProjectsAsync();
    Task<Project> AddAsync(Project project);
}
