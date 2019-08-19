using MemeGenerator.Infrastructure.Converters;
using MemeGenerator.Infrastructure.FileReaders;
using Microsoft.Extensions.DependencyInjection;

namespace MemeGenerator.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IFileReader, FileReader>();
            services.AddTransient<IBase64ImageEncoder, Base64ImageEncoder>();

            return services;
        }
    }
}
