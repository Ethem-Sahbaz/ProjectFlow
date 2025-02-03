using ProjectFlow.Application.Domain.Users;

namespace ProjectFlow.Application.Data.Repositories;
internal sealed class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users;

    public InMemoryUserRepository()
    {
        _users = SeedData();
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        var user = _users.FirstOrDefault(x => x.Id == id);

        return Task.FromResult(user);
    }

    private List<User> SeedData()
    {
        return new()
        {
            new User(Guid.Parse("5bc38e06-2deb-459e-8bb8-299daa4e3e20"), "Steve Fox", "s.fox@test.com"),
            new User(Guid.Parse("2c05da8e-47ab-4260-a7ec-5ec2342fd547"), "Jimmy Mcgill", "m.gill@test.com"),
            new User(Guid.Parse("2c05da8e-47ab-4260-a7ec-5ec2342fd548"), "Martin Owl", "m.owl@test.com")
        };

    }
}
