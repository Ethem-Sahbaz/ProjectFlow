using Microsoft.Extensions.DependencyInjection;
using ProjectFlow.Application.Data.Repositories;
using ProjectFlow.Application.Domain.ProjectMembers;
using ProjectFlow.Application.Domain.Projects;
using ProjectFlow.Application.Domain.Users;
using ProjectFlow.Application.Services.Projects;
using ProjectFlow.Application.Services.Projects.Interfaces;

namespace ProjectFlow.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IProjectRepository, InMemoryProjectRepository>();
        services.AddSingleton<IProjectMemberRepository, InMemoryProjectMemberRepository>();
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();

        services.AddSingleton<IProjectsReader, ProjectService>();
        services.AddSingleton<IProjectCreator, ProjectService>();
        services.AddSingleton<IProjectMemberReader, ProjectService>();
        services.AddSingleton<IProjectDeletor, ProjectService>();
        services.AddSingleton<IProjectJoinRequestCreator, ProjectJoinRequestManager>();
        services.AddSingleton<IProjectJoinRequestReader, ProjectJoinRequestManager>();
        return services;
    }
}
