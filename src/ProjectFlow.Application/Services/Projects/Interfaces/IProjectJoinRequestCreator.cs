using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectJoinRequestCreator
{
    Task<bool> CreateJoinRequestAsync(Guid userId,Guid projectId, CreateProjectJoinRequest request);
}
