namespace ProjectFlow.Contracts.Projects;
public sealed record ProjectJoinRequestResponse(Guid UserId, Guid ProjectId, string? Motivation);
