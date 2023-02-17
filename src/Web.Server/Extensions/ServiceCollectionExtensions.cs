using Serilog;
using Web.Application;

namespace Web.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, WebApplicationBuilder builder, IConfiguration config)
        {
            AddSeriLog(builder, config);
            AddRepos(services);
            AddServices(services);
        }
        public static void AddSeriLog(this WebApplicationBuilder builder, IConfiguration config)
        {
            var logger = new LoggerConfiguration()
              .ReadFrom.Configuration(config)
              .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
        }
        public static void AddRepos(IServiceCollection services)
        {
            //services.AddSingleton<IMysqlDataAccess, MysqlDataAccess>();
            //services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            //services.AddScoped<IClientRepo, ClientRepo>();
            //services.AddScoped<IUserRepo, UserRepo>();
        }
        public static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IUserServices, UserServices>();
        }
    }
}
