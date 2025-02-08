namespace ProjectFlow.Application.Domain.Projects;

// Accept/Decline Request on route /api/{projectid}/join-request/{requestid}
public sealed class JoinRequest
{
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

    public Guid Id { get; }
    public Guid UserId { get; init; }
    public Guid ProjectId { get; init; }
    public string? Motivation { get; init; }
}
