using ProjectFlow.Application.Shared;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectDeletor
{
    Task<Result> DeleteByIdAsync(Guid id, Guid userId);
}
