namespace ProjectFlow.Application.Domain.Users;
internal interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
}
