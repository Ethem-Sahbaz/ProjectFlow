namespace ProjectFlow.Contracts.Projects;
public sealed record UpdateProjectRequest(string Name, string Description, bool isPublic);
