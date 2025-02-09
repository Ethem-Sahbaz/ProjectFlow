using ProjectFlow.Application.Shared;

namespace ProjectFlow.Application.Domain.Projects.Errors;
internal static class ProjectErrors
{
    public static Error NotFound = new("Projects.NotFound", "Project could not be found");
}
