using ProjectFlow.Application.Domain.ProjectMembers;

namespace ProjectFlow.Application.Data.Repositories;
internal sealed class InMemoryProjectMemberRepository : IProjectMemberRepository
{
    private readonly List<ProjectMember> _projectMembers = new();

    public Task<bool> AddAsync(ProjectMember projectMember)
    {
        if (IsAlreadyMember(projectMember))
        {
            return Task.FromResult(false);
        }

        _projectMembers.Add(projectMember);

        return Task.FromResult(true);
    }

    public Task<IReadOnlyList<ProjectMember>> GetAllAsync(Guid projectId)
    {
        var members = _projectMembers
            .Where(x => x.ProjectId == projectId)
            .ToList();

        return Task.FromResult<IReadOnlyList<ProjectMember>>(members);
    }

    public Task<bool> IsProjectOwner(Guid projectId, Guid userId)
    {
        var member = _projectMembers.FirstOrDefault(m => m.UserId == userId &&  m.ProjectId == projectId);

        if (member is null)
            return Task.FromResult(false);

        if(member.Role != "Owner")
            return Task.FromResult(false);


        return Task.FromResult(true);
    }

    private bool IsAlreadyMember(ProjectMember projectMember)
    {
        return _projectMembers.Exists(p => p.UserId == projectMember.UserId && p.ProjectId == projectMember.ProjectId);
    }
}
