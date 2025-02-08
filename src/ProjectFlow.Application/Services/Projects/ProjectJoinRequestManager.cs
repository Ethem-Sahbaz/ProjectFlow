using ProjectFlow.Application.Domain.Projects;
using ProjectFlow.Application.Domain.Users;
using ProjectFlow.Application.Services.Projects.Interfaces;
using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects;
internal sealed class ProjectJoinRequestManager : IProjectJoinRequestCreator, IProjectJoinRequestReader
{
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;

    public ProjectJoinRequestManager(
        IUserRepository userRepository,
        IProjectRepository projectRepository)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;
    }

    public async Task<bool> CreateJoinRequestAsync(Guid userId, Guid projectId, CreateProjectJoinRequest request)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        // TODO: Replace with Result Pattern.
        if (user is null)
            return false;

        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project is null)
            return false;

        var joinRequest = new JoinRequest(Guid.NewGuid(), userId, projectId, request.Motivation);

        project.JoinRequests.Add(joinRequest);

        return true;
    }

    public async Task<IReadOnlyList<ProjectJoinRequestResponse>> GetProjectJoinRequestsAsync(Guid projectId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project is null)
            return new List<ProjectJoinRequestResponse>();

        var joinRequestResponses = project.JoinRequests
            .Select(r => new ProjectJoinRequestResponse(r.Id, r.ProjectId, r.Motivation))
            .ToList();

        return joinRequestResponses;
    }
}
