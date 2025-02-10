using ProjectFlow.Application.Shared;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectDeleter
{
    Task<Result> DeleteByIdAsync(Guid id, Guid userId);
}
