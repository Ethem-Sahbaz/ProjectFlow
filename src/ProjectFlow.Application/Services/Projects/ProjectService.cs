using ProjectFlow.Application.Domain.ProjectMembers;
using ProjectFlow.Application.Domain.Projects;
using ProjectFlow.Application.Mapping;
using ProjectFlow.Application.Services.Projects.Interfaces;
using ProjectFlow.Contracts.ProjectMembers;
using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects;
internal sealed class ProjectService : IProjectsReader, IProjectCreator
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectMemberRepository _projectMemberRepository;

    public ProjectService(
        IProjectRepository projectRepository,
        IProjectMemberRepository projectMemberRepository)
    {
        _projectRepository = projectRepository;
        _projectMemberRepository = projectMemberRepository;
    }

    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request)
    {
        Project project = new(Guid.NewGuid(), request.Name, request.Description, request.isPublic);

        await _projectRepository.AddAsync(project);

        return project.MapToResponse();
    }

    public async Task<IReadOnlyList<ProjectResponse>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();

        return projects
            .Select(p => p.MapToResponse())
            .ToList();
    }

    public async Task<IReadOnlyList<ProjectMemberResponse>> GetMembers(Guid projectId)
    {
        var members = await _projectMemberRepository.GetAllAsync(projectId);

        return members
            .Select(m => m.MapToResponse())
            .ToList();
    }
}
