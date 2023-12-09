using Core.DataAccess.SqlLite;
using Serilog;
using Web.Application;
using Web.Server.Filter.Security;

namespace Web.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, WebApplicationBuilder builder, IConfiguration config)
        {
            AddSecurity(services);
            AddSeriLog(builder, config);
            AddDbContext(services, config);
            AddRepos(services);
            AddServices(services);
        }
        public static void AddSecurity(IServiceCollection services)
        {
            services.AddSingleton<ApiKeyAuthorizationFilter>();
            services.AddSingleton<IApiKeyValidator, ApiKeyValidator>();
        }
        public static void AddSeriLog(this WebApplicationBuilder builder, IConfiguration config)
        {
            var logger = new LoggerConfiguration()
              .ReadFrom.Configuration(config)
              .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
        }
        public static void AddDbContext(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<DbContext>();
        }
        public static void AddRepos(IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepo>();
        }
        public static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IUserServices, UserServices>();
        }
    }
}
