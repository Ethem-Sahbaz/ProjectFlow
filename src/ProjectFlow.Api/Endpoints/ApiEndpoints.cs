namespace ProjectFlow.Api.Endpoints;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Projects
    {
        private const string Base = $"{ApiBase}/projects";

        public const string Get = Base;

        public const string Post = Base;

        public const string GetById = $"{Base}/{{id:guid}}";

        public const string GetProjectMembers = $"{Base}/{{id:guid}}/projectmembers";
    }

    public static class Identity
    {
        private const string Base =$"{ApiBase}/identity";

        public const string Token = $"{Base}/token";
    }
}
