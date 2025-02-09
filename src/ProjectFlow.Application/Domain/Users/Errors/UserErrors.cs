using ProjectFlow.Application.Shared;

namespace ProjectFlow.Application.Domain.Users.Errors;
internal static class UserErrors
{
    public static Error NotFound = new("Users.NotFound", "Could not find a user with this id");
}
