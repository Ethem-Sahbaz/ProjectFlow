namespace ProjectFlow.Application.Domain.ProjectMembers;
internal sealed class ProjectMember
{
    public ProjectMember(
        Guid projectId,
        Guid userId,
        bool isOwner,
        string role)
    {
        ProjectId = projectId;
        UserId = userId;
        IsOwner = isOwner;
        Role = role;
    }

    public Guid UserId { get; init; }
    public Guid ProjectId { get; init; }
    public bool IsOwner { get; init; }
    public string Role { get; init; }
}
