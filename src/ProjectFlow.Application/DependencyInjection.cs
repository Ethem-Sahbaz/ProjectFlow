using Microsoft.Extensions.DependencyInjection;
using ProjectFlow.Application.Data.Repositories;
using ProjectFlow.Application.Domain.Projects;
using ProjectFlow.Application.Services.Projects;
using ProjectFlow.Application.Services.Projects.Interfaces;

namespace ProjectFlow.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IProjectRepository, InMemoryProjectRepository>();
        services.AddSingleton<IProjectsReader, ProjectService>();
        services.AddSingleton<IProjectCreator, ProjectService>();

        return services;
    }
}
