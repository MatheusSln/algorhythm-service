using AlgorhythmService.Data.Repository;
using AlgorhythmService.Handler.Exercice.Repository;
using AlgorhythmService.Handler.Handler;
using AlgorhythmService.Handler.Handler.Interface;
using AlgorhythmService.Shared;
using AlgorhythmService.Shared.Database;
using AlgorhythmService.Shared.Database.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AlgorhythmService
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        private static void RegisterConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public static void Main(string[] args)
        {
            RegisterConfiguration();

            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var appSettingsRoot = new AppSettingsRoot();
            Configuration.Bind(appSettingsRoot);

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    services.AddSingleton(appSettingsRoot);

                    ConfigureHandler(services);
                    ConfigureRepository(services);
                    ConfigureDB(services);
                });
        }

        private static void ConfigureHandler(IServiceCollection services)
        {
            services.AddTransient<IDeletionHandler, DeletionHandler>();
        }

        private static void ConfigureRepository(IServiceCollection services)
        {
            services.AddTransient<IExerciceRepository, ExerciceRepository>();
        }

        private static void ConfigureDB(IServiceCollection services)
        {
            services.AddTransient<DbSession>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
