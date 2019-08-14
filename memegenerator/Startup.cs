using System;
using MemeGenerator.BLL.Services;
using MemeGenerator.DAL;
using MemeGenerator.DAL.Configs;
using MemeGenerator.DAL.Converters;
using MemeGenerator.DAL.FileReaders;
using MemeGenerator.DAL.MigrationsChecker;
using MemeGenerator.DAL.Providers;
using MemeGenerator.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MemeGenerator.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MemeGeneratorDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("MemeGenerator.DAL")));

            services.Configure<MemesConfig>(Configuration);

            services.AddTransient<IInitialMemesProvider, InitialMemesProvider>();
            services.AddTransient<IInitialMemesPopulator, InitialMemesPopulator>();
            services.AddTransient<IMemeRepository, MemeRepository>();
            services.AddTransient<IMigrationsChecker, MigrationsChecker>();
            services.AddTransient<IFileReader, FileReader>();
            services.AddTransient<IBase64Converter, Base64Converter>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    services.GetRequiredService<IInitialMemesPopulator>().Initialize().Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Initial meme population has failed.");

                    throw;
                }
            }
        }
    }
}
