using ProjectFlow.Application.Domain.ProjectMembers;

namespace ProjectFlow.Application.Data.Repositories;
internal sealed class InMemoryProjectMemberRepository : IProjectMemberRepository
{
    private readonly List<ProjectMember> _projectMembers;

    public InMemoryProjectMemberRepository()
    {
        _projectMembers = SeedData();
    }

    public Task<IReadOnlyList<ProjectMember>> GetAllAsync(Guid projectId)
    {
        return Task.FromResult<IReadOnlyList<ProjectMember>>(_projectMembers);
    }

    private List<ProjectMember> SeedData()
    {
        return new List<ProjectMember>() 
        {
            new(Guid.NewGuid(), Guid.NewGuid(), true,"Developer"),
            new(Guid.NewGuid(), Guid.NewGuid(), false, "HR"),
            new(Guid.NewGuid(), Guid.NewGuid(), false, "Student")
        };
    }
}
