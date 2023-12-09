using Core.DataAccess.SqlLite;
using Microsoft.AspNetCore.ResponseCompression;
using Web.Server.Extensions;
using Web.Server.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var env = builder.Environment;
var config = builder.Configuration;
config.SetBasePath(Directory.GetCurrentDirectory());
config.AddEnvironmentVariables();

// Add services to the container.
services.AddControllers()
    .AddJsonOptions( options => 
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });
services.AddSwagger(config);
services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
    options.Providers.Add<BrotliCompressionProvider>();
});
services.AddControllersWithViews()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
services.AddApplicationServices(builder, config);
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// ensure database and tables exist
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DbContext>();
    await context.Init();
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerDashboard(config);

// global request handler
app.UseMiddleware<RequestHandler>();

app.Run();
