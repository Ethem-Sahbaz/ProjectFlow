using ProjectFlow.Application.Domain.Projects;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectsReader
{
    Task<IReadOnlyList<Project>> GetAllProjectsAsync();
}
