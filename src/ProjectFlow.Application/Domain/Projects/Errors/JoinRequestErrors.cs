using ProjectFlow.Application.Shared;

namespace ProjectFlow.Application.Domain.Projects.Errors;
internal static class JoinRequestErrors
{
    public static Error NotFound = new("JoinRequests.NotFound", "Could not find a join request with this id.");
}
