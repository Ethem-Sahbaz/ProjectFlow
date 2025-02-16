namespace ProjectFlow.Application.Domain.ProjectMembers;
/// <summary>
/// Represents a member of a project with a specific role.
/// </summary>
internal sealed class ProjectMember
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectMember"/> class.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project.</param>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="role">The role of the user in the project.</param>
    public ProjectMember(
        Guid projectId,
        Guid userId,
        string role)
    {
        ProjectId = projectId;
        UserId = userId;
        Role = role;
    }

    /// <summary>
    /// Gets the unique identifier of the user.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the unique identifier of the project.
    /// </summary>
    public Guid ProjectId { get; init; }

    /// <summary>
    /// Gets the role of the user in the project.
    /// </summary>
    public string Role { get; init; }
}
