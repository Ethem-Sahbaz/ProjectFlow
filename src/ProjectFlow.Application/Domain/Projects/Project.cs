namespace ProjectFlow.Application.Domain.Projects;
public sealed class Project
{
    public Project(
        Guid id,
        string name,
        string description,
        bool isPublic)
    {
        Id = id;
        Name = name;
        Description = description;
        IsPublic = isPublic;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public bool IsPublic { get; init; }
}
