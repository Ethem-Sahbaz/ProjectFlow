using ProjectFlow.Application.Shared;
using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectJoinRequestCreator
{
    Task<Result> CreateJoinRequestAsync(Guid userId,Guid projectId, CreateProjectJoinRequest request);
}
