using MemeGenerator.Infrastructure.Configs;
using MemeGenerator.Infrastructure.FileReaders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MemeGenerator.Infrastructure
{
    public static class MemeGeneratorRegistration
    {
        public static IServiceCollection RegisterMemeGenerator(
            this IServiceCollection services, 
            IConfiguration configuration)
        {

            services.Configure<MemesConfig>(configuration);

            services.AddTransient<IFileReader, FileReader>();

            return services;
        }
    }
}
