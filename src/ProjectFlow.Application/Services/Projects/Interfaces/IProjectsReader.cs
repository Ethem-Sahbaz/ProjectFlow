﻿using ProjectFlow.Contracts.Projects;

namespace ProjectFlow.Application.Services.Projects.Interfaces;
public interface IProjectsReader
{
    Task<IReadOnlyList<ProjectResponse>> GetAllProjectsAsync();
}
