namespace ProjectFlow.Contracts.ProjectMembers;
public sealed record ProjectMemberResponse(Guid ProjectId, Guid UserId, bool IsOwner, string Role);