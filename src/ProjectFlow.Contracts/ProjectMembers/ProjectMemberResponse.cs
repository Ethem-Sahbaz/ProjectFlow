namespace ProjectFlow.Contracts.ProjectMembers;
public sealed record ProjectMemberResponse(Guid ProjectId, Guid UserId,string name ,bool IsOwner, string Role);