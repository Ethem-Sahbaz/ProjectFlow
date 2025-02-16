namespace ProjectFlow.Application.Domain.Users;
/// <summary>
/// Defines the contract for a repository that manages <see cref="User"/> entities.
/// </summary>
internal interface IUserRepository
{
    /// <summary>
    /// Retrieves a <see cref="User"/> by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>Returns the <see cref="User"/> if found; otherwise, <c>null</c>.</returns>
    Task<User?> GetByIdAsync(Guid id);
}
