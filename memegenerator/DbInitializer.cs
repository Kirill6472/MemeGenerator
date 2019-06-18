using System.Collections.Generic;
using System.IO;
using System.Linq;
using MemeGenerator.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            List<ImageTemplateList> imageTemplates = JsonConvert.DeserializeObject<List<ImageTemplateList>>
                (File.ReadAllText(dbInitializerSetting.PathToMemeTemplatesConfig));

            context.AddRange(imageTemplates);

            context.SaveChanges();
        }

    }

    public class ImageTemplateList
    {
        public string Folder { get; set; }
        public List<ImageTemplate> ImageTemplate { get; set; }
    }

}
