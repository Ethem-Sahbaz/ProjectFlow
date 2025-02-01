namespace ProjectFlow.Application.Domain.ProjectMembers;
internal sealed class ProjectMember
{
    public ProjectMember(
        Guid userId,
        Guid projectId,
        bool isOwner,
        string role)
    {
        UserId = userId;
        ProjectId = projectId;
        IsOwner = isOwner;
        Role = role;
    }

    public Guid UserId { get; init; }
    public Guid ProjectId { get; init; }
    public bool IsOwner { get; init; }
    public string Role { get; init; }
}
