using ProjectFlow.Application.Shared;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectMemberDeleter
{
    Task<Result> RemoveProjectMemberAsync(Guid projectId, Guid requestUserId, Guid targetUserId);
}
