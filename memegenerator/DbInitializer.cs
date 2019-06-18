using System;
using System.Collections.Generic;
using MemeGenerator.Models;
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
            string dataText = System.IO.File.ReadAllText(dbInitializerSetting.Path);

            List<ImageTemplate> imageTemplates = JsonConvert.DeserializeObject<List<ImageTemplate>>(dataText);

            context.Database.EnsureCreated();

            context.ImageTemplates.Add(new ImageTemplate {Name = "s", Desctiption = "sw", Folder = "ASd"});

            context.SaveChanges();
        }
    }
}
