namespace ProjectFlow.Application.Domain.ProjectMembers;
internal interface IProjectMemberRepository
{
    Task<IReadOnlyList<ProjectMember>> GetAllAsync(Guid projectId);
}
