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
        var members = _projectMembers
            .Where(x => x.ProjectId == projectId)
            .ToList();

        return Task.FromResult<IReadOnlyList<ProjectMember>>(members);
    }

    private List<ProjectMember> SeedData()
    {
        return new List<ProjectMember>() 
        {
            new(Guid.Parse("0b0d9dab-cc1d-4ae9-a3f8-bacbeaa56280"), Guid.NewGuid(), true,"Developer"),
            new(Guid.Parse("0b0d9dab-cc1d-4ae9-a3f8-bacbeaa56280"), Guid.NewGuid(), false, "HR"),
            new(Guid.Parse("0b0d9dab-cc1d-4ae9-a3f8-bacbeaa56280"), Guid.NewGuid(), false, "Student")
        };
    }
}
