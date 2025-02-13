using ProjectFlow.Application.Domain.ProjectMembers;

namespace ProjectFlow.Application.Data.Repositories;
internal sealed class InMemoryProjectMemberRepository : IProjectMemberRepository
{
    private readonly List<ProjectMember> _projectMembers = new();

    public Task AddAsync(ProjectMember projectMember)
    {
        _projectMembers.Add(projectMember);

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<ProjectMember>> GetAllAsync(Guid projectId)
    {
        var members = _projectMembers
            .Where(x => x.ProjectId == projectId)
            .ToList();

        return Task.FromResult<IReadOnlyList<ProjectMember>>(members);
    }

    public Task<bool> IsProjectOwnerAsync(Guid projectId, Guid userId)
    {
        var member = _projectMembers.FirstOrDefault(m => m.UserId == userId &&  m.ProjectId == projectId);

        if (member is null)
            return Task.FromResult(false);

        if(member.Role != "Owner")
            return Task.FromResult(false);


        return Task.FromResult(true);
    }

    public Task<bool> IsAlreadyMember(Guid projectId, Guid userId)
    {
        var isMember = _projectMembers.Exists(p => p.UserId == userId && p.ProjectId == projectId);

        return Task.FromResult(isMember);
    }
}
