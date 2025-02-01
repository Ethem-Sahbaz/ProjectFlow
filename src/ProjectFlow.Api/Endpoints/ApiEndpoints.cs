﻿namespace ProjectFlow.Api.Endpoints;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Projects
    {
        private const string Base = $"{ApiBase}/projects";

        public const string Get = Base;

        public const string Post = Base;

    }
}
