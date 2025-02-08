namespace ProjectFlow.Application.Domain.ProjectMembers;
internal sealed class ProjectMember
{
    public ProjectMember(
        Guid projectId,
        Guid userId,
        string role)
    {
        ProjectId = projectId;
        UserId = userId;
        Role = role;
    }

    public Guid UserId { get; init; }
    public Guid ProjectId { get; init; }
    public string Role { get; init; }
}
