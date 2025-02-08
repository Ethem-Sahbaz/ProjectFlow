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

    private bool IsAlreadyMember(ProjectMember projectMember)
    {
        return _projectMembers.Exists(p => p.UserId == projectMember.UserId && p.ProjectId == projectMember.ProjectId);
    }
}
