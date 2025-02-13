namespace ProjectFlow.Contracts.Projects;
public sealed record ProjectJoinRequestResponse(Guid Id, Guid UserId, Guid ProjectId, string? Motivation);
