namespace ProjectFlow.Contracts.Projects;
public sealed record ProjectResponse(Guid Id,string Name, string? Description, bool isPublic);
