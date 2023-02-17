using Swashbuckle.AspNetCore.SwaggerUI;
using Web.Server.Extensions;
using Web.Server.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var env = builder.Environment;
var config = builder.Configuration;
config.SetBasePath(Directory.GetCurrentDirectory());
config.AddEnvironmentVariables();

// Add services to the container.

services.AddControllers();
services.AddSwagger(config);
services.AddApplicationServices(builder, config);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerDashboard(config);

// global request handler
app.UseMiddleware<RequestHandler>();

app.Run();
