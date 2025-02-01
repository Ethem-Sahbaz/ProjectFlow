namespace ProjectFlow.Application.Domain.Users;
internal sealed class User
{
    public User(
        Guid id,
        string name,
        string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
}
