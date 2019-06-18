using System.IO;
using MemeGenerator.Models;
using MemeGenerator.Servises;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MemeGenerator
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DbInitializerSetting dbInitializerSetting;

        public DbInitializer(IOptionsMonitor<DbInitializerSetting> imageTemplateAccessor)
        {
            dbInitializerSetting = imageTemplateAccessor.CurrentValue;
        }

        public void Initialize(MemeGeneratorDbContext context)
        {
            ImageTemplateList imageTemplates = JsonConvert.DeserializeObject<ImageTemplateList>
                (File.ReadAllText(dbInitializerSetting.PathToMemeTemplatesConfig));

            foreach (var image in imageTemplates.ImageTemplate)
            {
                image.Folder = imageTemplates.Folder;
                context.AddRange(image);
            }

            context.SaveChanges();
        }
    }
}
