namespace ProjectFlow.Application.Domain.Users;
/// <summary>
/// Represents a user in the application.
/// </summary>
internal sealed class User
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the user.</param>
    /// <param name="name">The name of the user.</param>
    /// <param name="email">The email address of the user.</param>
    public User(
        Guid id,
        string name,
        string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    /// <summary>
    /// Gets the unique identifier for the user.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the name of the user.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    public string Email { get; init; }
}
