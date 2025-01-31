using Microsoft.AspNetCore.Http.HttpResults;
using ProjectFlow.Application;
using ProjectFlow.Application.Services.Projects.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/projects", async (IProjectsReader reader) =>
{
    var projects = await reader.GetAllProjectsAsync();

    return Results.Ok(projects);
})
.WithName("GetAllProjects")
.WithOpenApi();

app.Run();

