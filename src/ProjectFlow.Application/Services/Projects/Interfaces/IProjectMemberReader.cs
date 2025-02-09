using ProjectFlow.Application.Shared;
using ProjectFlow.Contracts.ProjectMembers;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectMemberReader
{
    Task<Result<IReadOnlyList<ProjectMemberResponse>>> GetMembers(Guid projectId);
}
