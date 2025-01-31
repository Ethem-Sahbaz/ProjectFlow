namespace ProjectFlow.Application.Domain.Projects;
public interface IProjectRepository
{
    Task<IReadOnlyList<Project>> GetAll();
}
