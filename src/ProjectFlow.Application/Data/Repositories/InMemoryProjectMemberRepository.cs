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
            new(Guid.Parse("0b0d9dab-cc1d-4ae9-a3f8-bacbeaa56280"), Guid.Parse("5bc38e06-2deb-459e-8bb8-299daa4e3e20"), true,"Developer"),
            new(Guid.Parse("0b0d9dab-cc1d-4ae9-a3f8-bacbeaa56280"), Guid.Parse("2c05da8e-47ab-4260-a7ec-5ec2342fd547"), false, "HR"),
            new(Guid.Parse("0b0d9dab-cc1d-4ae9-a3f8-bacbeaa56280"), Guid.Parse("2c05da8e-47ab-4260-a7ec-5ec2342fd548"), false, "Student")
        };
    }
}
