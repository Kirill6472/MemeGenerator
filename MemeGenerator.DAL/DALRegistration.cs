using MemeGenerator.Core;
using MemeGenerator.DAL.Configs;
using MemeGenerator.DAL.MigrationChecker;
using MemeGenerator.DAL.Providers;
using MemeGenerator.DAL.Repositories;
using MemeGenerator.DAL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MemeGenerator.DAL
{
    public static class DALRegistration
    {
        public static IServiceCollection RegisterDAL(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<MemeGeneratorDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("MemeGenerator.DAL")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MemeGeneratorDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<MemesConfig>(configuration);

            services.AddTransient<IInitialMemesProvider, InitialMemesProvider>();
            services.AddTransient<IInitialMemesPopulator, InitialMemesPopulator>();
            services.AddTransient<IMemeRepository, MemeRepository>();
            services.AddTransient<IMigrationsChecker, MigrationsChecker>();
            
            return services;
        }

    }
}
