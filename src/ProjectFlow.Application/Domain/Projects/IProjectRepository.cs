namespace ProjectFlow.Application.Domain.Projects;
/// <summary>
/// Defines the repository interface for managing projects.
/// </summary>
public interface IProjectRepository
{
    /// <summary>
    /// Retrieves all projects asynchronously.
    /// </summary>
    /// <returns>Returns a read-only list of projects.</returns>
    Task<IReadOnlyList<Project>> GetAllAsync();

    /// <summary>
    /// Retrieves a project by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the project.</param>
    /// <returns>Returns the project if found; otherwise, null.</returns>
    Task<Project?> GetByIdAsync(Guid id);

    /// <summary>
    /// Retrieves all public projects asynchronously.
    /// </summary>
    /// <returns>Returns a read-only list of public projects.</returns>
    Task<IReadOnlyList<Project>> GetPublicProjectsAsync();

    /// <summary>
    /// Adds a new project asynchronously.
    /// </summary>
    /// <param name="project">The project to add.</param>
    /// <returns>Returns the added project.</returns>
    Task<Project> AddAsync(Project project);

    /// <summary>
    /// Deletes a project by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the project to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteByIdAsync(Guid id);
}
