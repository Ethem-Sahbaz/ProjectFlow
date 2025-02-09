﻿namespace ProjectFlow.Application.Domain.ProjectMembers;
internal interface IProjectMemberRepository
{
    Task<IReadOnlyList<ProjectMember>> GetAllAsync(Guid projectId);
    Task<bool> AddAsync(ProjectMember projectMember);
    Task<bool> IsProjectOwner(Guid projectId, Guid userId);
}
