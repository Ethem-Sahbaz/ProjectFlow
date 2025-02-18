namespace ProjectFlow.Application.Domain.ProjectMembers;
/// <summary>
/// Interface for managing project members in the repository.
/// </summary>
internal interface IProjectMemberRepository
{
    /// <summary>
    /// Retrieves all project members for a given project.
    /// </summary>
    /// <param name="projectId">The ID of the project.</param>
    /// <returns>A read-only list of project members.</returns>
    Task<IReadOnlyList<ProjectMember>> GetAllAsync(Guid projectId);

    /// <summary>
    /// Adds a new project member to the repository.
    /// </summary>
    /// <param name="projectMember">The project member to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(ProjectMember projectMember);

    /// <summary>
    /// Checks if a user is the owner of a project.
    /// </summary>
    /// <param name="projectId">The ID of the project.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>Returns a boolean indicating whether the user is the project owner.</returns>
    Task<bool> IsProjectOwnerAsync(Guid projectId, Guid userId);

    /// <summary>
    /// Checks if a user is already a member of a project.
    /// </summary>
    /// <param name="projectId">The ID of the project.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>Returns a boolean indicating whether the user is already a member of the project.</returns>
    Task<bool> IsAlreadyMember(Guid projectId, Guid userId);

    /// <summary>
    /// Deletes a project member from a project.
    /// </summary>
    /// <param name="projectId">The ID of the project.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteAsync(Guid projectId, Guid userId);
}
