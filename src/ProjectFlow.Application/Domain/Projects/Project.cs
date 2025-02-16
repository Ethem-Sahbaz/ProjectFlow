namespace ProjectFlow.Application.Domain.Projects;
/// <summary>
/// Represents a project within the application.
/// </summary>
public sealed class Project
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Project"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the project.</param>
    /// <param name="createdByUserId">The unique identifier of the user who created the project.</param>
    /// <param name="name">The name of the project.</param>
    /// <param name="description">The description of the project.</param>
    /// <param name="isPublic">Indicates whether the project is public.</param>
    public Project(
        Guid id,
        Guid createdByUserId,
        string name,
        string? description,
        bool isPublic)
    {
        Id = id;
        Name = name;
        Description = description;
        IsPublic = isPublic;
        CreatedByUserId = createdByUserId;
    }

    /// <summary>
    /// Gets the unique identifier of the project.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the unique identifier of the user who created the project.
    /// </summary>
    public Guid CreatedByUserId { get; init; }

    /// <summary>
    /// Gets the name of the project.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets the description of the project.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets a value indicating whether the project is public.
    /// </summary>
    public bool IsPublic { get; init; }

    /// <summary>
    /// Gets the list of join requests associated with the project.
    /// </summary>
    public List<JoinRequest> JoinRequests { get; init; } = new();
}
