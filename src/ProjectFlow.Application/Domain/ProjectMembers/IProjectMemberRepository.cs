namespace ProjectFlow.Application.Domain.ProjectMembers;
internal interface IProjectMemberRepository
{
    Task<IReadOnlyList<ProjectMember>> GetAllAsync(Guid projectId);
    Task AddAsync(ProjectMember projectMember);
    Task<bool> IsProjectOwnerAsync(Guid projectId, Guid userId);
    Task<bool> IsAlreadyMember(Guid projectId, Guid userId);
}
