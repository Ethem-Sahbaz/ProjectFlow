namespace ProjectFlow.Application.Domain.Projects;
public sealed class Project
{
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

    public Guid Id { get; init; }
    public Guid CreatedByUserId { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public bool IsPublic { get; init; }
}
