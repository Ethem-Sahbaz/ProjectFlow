﻿using ProjectFlow.Application.Shared;

namespace ProjectFlow.Application.Domain.ProjectMembers.Errors;
public static class ProjectMemberErrors
{
    public static Error NotOwner = new("ProjectMembers.NotOwner", "Required to be owner.");

    public static Error MemberExists = new("ProjectMembers.Exists", "User is already a member.");
}
