using ProjectFlow.Application.Shared;

namespace ProjectFlow.Application.Domain.ProjectMembers.Errors;
public static class ProjectMemberErrors
{
    public static Error NotOwner = new("ProjectMembers.NotOwner", "Required to be owner.");
}
