using ProjectFlow.Application.Domain.ProjectMembers;
using ProjectFlow.Application.Domain.Projects;
using ProjectFlow.Contracts.ProjectMembers;
using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Mapping;
internal static class ContractMapping
{
    public static ProjectResponse MapToResponse(this Project project)
    {
        return new ProjectResponse(
            project.Id,
            project.Name,
            project.Description,
            project.IsPublic);
    }
    public static ProjectMemberResponse MapToResponse(this ProjectMember member)
    {
        return new ProjectMemberResponse(
            member.UserId,
            member.ProjectId,
            member.IsOwner,
            member.Role);
    }
}
