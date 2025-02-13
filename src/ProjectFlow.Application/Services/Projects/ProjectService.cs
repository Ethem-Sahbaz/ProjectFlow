using ProjectFlow.Application.Domain.ProjectMembers;
using ProjectFlow.Application.Domain.ProjectMembers.Errors;
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
internal sealed class ProjectService 
    : IProjectsReader, IProjectCreator, IProjectMemberReader, IProjectDeleter, IProjectUpdater
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

    public async Task<Result> DeleteByIdAsync(Guid id, Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
            return Result.Failure(UserErrors.NotFound);

        var project = await _projectRepository.GetByIdAsync(id);

        if (project is null)
            return Result.Failure(ProjectErrors.NotFound);

        var isOwner = await _projectMemberRepository.IsProjectOwnerAsync(id, userId);

        if (!isOwner)
            return Result.Failure(ProjectMemberErrors.NotOwner);

        await _projectRepository.DeleteByIdAsync(id);

        return Result.Success();
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

    public async Task<Result<ProjectResponse>> UpdateAsync(UpdateProjectRequest request, Guid projectId, Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
            return Result.Failure<ProjectResponse>(UserErrors.NotFound);

        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project is null)
            return Result.Failure<ProjectResponse>(ProjectErrors.NotFound);

        var isOwner = await _projectMemberRepository.IsProjectOwnerAsync(projectId, userId);

        if (!isOwner)
            return Result.Failure<ProjectResponse>(ProjectMemberErrors.NotOwner);

        var updatedProject = new Project(
            project.Id,
            project.CreatedByUserId,
            request.Name,
            request.Description,
            request.isPublic);

        updatedProject.JoinRequests.AddRange(project.JoinRequests);

        project.JoinRequests.Clear();

        await _projectRepository.DeleteByIdAsync(projectId);

        await _projectRepository.AddAsync(updatedProject);

        var projectResponse = new ProjectResponse(
            updatedProject.Id,
            updatedProject.Name,
            updatedProject.Description,
            updatedProject.IsPublic);

        return Result.Success(projectResponse);
    }

}
