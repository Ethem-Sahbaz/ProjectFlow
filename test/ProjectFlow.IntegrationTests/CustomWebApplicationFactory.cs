using Microsoft.AspNetCore.Mvc.Testing;

namespace ProjectFlow.IntegrationTests;
public class CustomWebApplicationFactory<TProgram> :
    WebApplicationFactory<TProgram> where TProgram : class;
