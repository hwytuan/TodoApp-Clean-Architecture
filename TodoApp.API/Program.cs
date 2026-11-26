using System.Reflection;
using Microsoft.OpenApi.Models;
using TodoApp.Application.Features.TodoItems.Commands;
using TodoApp.Application.Features.TodoItems.Queries;
using TodoApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TodoApp API",
        Version = "v1",
        Description = "API quản lý danh sách công việc theo kiến trúc Clean Architecture & CQRS"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<GetTodoItemsHandler>();
builder.Services.AddScoped<GetTodoItemByIdHandler>();
builder.Services.AddScoped<CreateTodoItemCommandHandler>();
builder.Services.AddScoped<UpdateTodoItemCommandHandler>();
builder.Services.AddScoped<DeleteTodoItemCommandHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
