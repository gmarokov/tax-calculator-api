using System.Reflection;
using FluentValidation;
using MediatR;
using Tax.Calculator.Api.Application.Behaviors;
using Tax.Calculator.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddMediatR(Assembly.GetExecutingAssembly())
    .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
    .AddTransient<ExceptionHandlerMiddleware>()
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlerMiddleware>()
    .UseHttpsRedirection()
    .UseAuthorization();

app.MapControllers();
app.Run();

/// Partial class used for WebApplicationFactory integration testing
public partial class Program { }
