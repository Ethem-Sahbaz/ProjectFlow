namespace ProjectFlow.Contracts.ProjectMembers;
public sealed record ProjectMemberResponse(Guid UserId, Guid ProjectId, bool IsOwner, string Role);