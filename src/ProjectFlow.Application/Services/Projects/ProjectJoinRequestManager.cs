using ProjectFlow.Application.Domain.ProjectMembers;
using ProjectFlow.Application.Domain.ProjectMembers.Errors;
using ProjectFlow.Application.Domain.Projects;
using ProjectFlow.Application.Domain.Projects.Errors;
using ProjectFlow.Application.Domain.Users;
using ProjectFlow.Application.Domain.Users.Errors;
using ProjectFlow.Application.Services.Projects.Interfaces;
using ProjectFlow.Application.Shared;
using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects;
internal sealed class ProjectJoinRequestManager 
    : IProjectJoinRequestCreator, IProjectJoinRequestReader, IProjectJoinRequestHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectMemberRepository _projectMemberRepository;

    public ProjectJoinRequestManager(
        IUserRepository userRepository,
        IProjectRepository projectRepository,
        IProjectMemberRepository projectMemberRepository)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _projectMemberRepository = projectMemberRepository;
    }

    public async Task<Result> CreateJoinRequestAsync(Guid userId, Guid projectId, CreateProjectJoinRequest request)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
            return Result.Failure(UserErrors.NotFound);

        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project is null)
            return Result.Failure(ProjectErrors.NotFound);

        var joinRequest = new JoinRequest(Guid.NewGuid(), userId, projectId, request.Motivation);

        project.JoinRequests.Add(joinRequest);

        return Result.Success();
    }

    public async Task<Result<IReadOnlyList<ProjectJoinRequestResponse>>> GetProjectJoinRequestsAsync(Guid projectId, Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
            return Result.Failure<IReadOnlyList<ProjectJoinRequestResponse>>(UserErrors.NotFound);

        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project is null)
            return Result.Failure<IReadOnlyList<ProjectJoinRequestResponse>>(ProjectErrors.NotFound);

        var isOwner = await _projectMemberRepository.IsProjectOwner(projectId, userId);

        if (!isOwner)
            return Result.Failure<IReadOnlyList<ProjectJoinRequestResponse>>(ProjectMemberErrors.NotOwner);

        var joinRequestResponses = project.JoinRequests
            .Select(r => new ProjectJoinRequestResponse(r.Id, r.ProjectId, r.Motivation))
            .ToList();

        return joinRequestResponses;
    }

    public async Task<Result> HandleRequestAsync(
        HandleProjectJoinRequest handleRequest,
        Guid projectId,
        Guid userId,
        Guid joinRequestId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
            return Result.Failure(UserErrors.NotFound);

        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project is null)
            return Result.Failure(ProjectErrors.NotFound);

        var isOwner = await _projectMemberRepository.IsProjectOwner(projectId, userId);

        if (!isOwner)
            return Result.Failure(ProjectMemberErrors.NotOwner);

        var joinRequest = project.JoinRequests.FirstOrDefault(x => x.Id == joinRequestId);

        if (joinRequest is null)
        {
            return Result.Failure(JoinRequestErrors.NotFound);
        }

        if (!handleRequest.IsApproved)
        {
            project.JoinRequests.Remove(joinRequest);

            return Result.Success();
        }
        var newProjectMember = new ProjectMember(projectId, joinRequest.UserId, "Member");

        await _projectMemberRepository.AddAsync(newProjectMember);

        project.JoinRequests.Remove(joinRequest);

        return Result.Success();
    }
}
