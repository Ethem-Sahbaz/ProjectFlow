using ProjectFlow.Contracts.ProjectMembers;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectMemberReader
{
    Task<IReadOnlyList<ProjectMemberResponse>> GetMembers(Guid projectId);
}
