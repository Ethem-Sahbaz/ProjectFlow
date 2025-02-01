namespace ProjectFlow.Contracts.Projects;
public sealed record CreateProjectRequest(string Name, string? Description, bool isPublic);
