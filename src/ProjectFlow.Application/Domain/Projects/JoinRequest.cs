namespace ProjectFlow.Application.Domain.Projects;

/// <summary>
/// Represents a request to join a project.
/// </summary>
public sealed class JoinRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JoinRequest"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the join request.</param>
    /// <param name="userId">The unique identifier for the user making the request.</param>
    /// <param name="projectId">The unique identifier for the project to join.</param>
    /// <param name="motivation">The motivation for joining the project.</param>
    public JoinRequest(
        Guid id,
        Guid userId,
        Guid projectId,
        string? motivation)
    {
        Id = id;
        UserId = userId;
        ProjectId = projectId;
        Motivation = motivation;
    }

    /// <summary>
    /// Gets the unique identifier for the join request.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Gets the unique identifier for the user making the request.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the unique identifier for the project to join.
    /// </summary>
    public Guid ProjectId { get; init; }

    /// <summary>
    /// Gets the motivation for joining the project.
    /// </summary>
    public string? Motivation { get; init; }
}
