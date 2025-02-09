using ProjectFlow.Application.Domain.ProjectMembers;
using ProjectFlow.Application.Domain.Projects;
using ProjectFlow.Application.Domain.Projects.Errors;
using ProjectFlow.Application.Domain.Users;
using ProjectFlow.Application.Domain.Users.Errors;
using ProjectFlow.Application.Mapping;
using ProjectFlow.Application.Services.Projects.Interfaces;
using ProjectFlow.Application.Shared;
using ProjectFlow.Contracts.ProjectMembers;
using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects;
internal sealed class ProjectService : IProjectsReader, IProjectCreator, IProjectMemberReader
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectMemberRepository _projectMemberRepository;
    private readonly IUserRepository _userRepository;

    public ProjectService(
        IProjectRepository projectRepository,
        IProjectMemberRepository projectMemberRepository,
        IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _projectMemberRepository = projectMemberRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<ProjectResponse>> CreateAsync(Guid userId, CreateProjectRequest request)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            return Result.Failure<ProjectResponse>(UserErrors.NotFound);
        }

        Project project = new(Guid.NewGuid(), userId, request.Name, request.Description, request.isPublic);

        await _projectRepository.AddAsync(project);

        var projectMember = new ProjectMember(project.Id, userId, "Owner");

        await _projectMemberRepository.AddAsync(projectMember);

        return project.MapToResponse();
    }

    public async Task<IReadOnlyList<ProjectResponse>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();

        return projects
            .Select(p => p.MapToResponse())
            .ToList();
    }

    public async Task<Result<IReadOnlyList<ProjectMemberResponse>>> GetMembers(Guid projectId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project is null)
        {
            return Result.Failure<IReadOnlyList<ProjectMemberResponse>>(ProjectErrors.NotFound);
        }

        var members = await _projectMemberRepository.GetAllAsync(projectId);

        var projectMemberResponses = new List<ProjectMemberResponse>();

        // For Queries there is no need to map domain model. Return clients needed data.
        // In case of use of a db.
        foreach (var member in members)
        {
            var user = await _userRepository.GetByIdAsync(member.UserId);

            if (user is null)
                continue;

            ProjectMemberResponse projectMemberResponse = new(member.ProjectId, user.Id, user.Name, member.Role);

            projectMemberResponses.Add(projectMemberResponse);
        }

        return projectMemberResponses;
    }
}
