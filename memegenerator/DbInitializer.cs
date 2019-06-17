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
        
        public void Initialize()
        {
            string dataText = System.IO.File.ReadAllText(dbInitializerSetting.Path);

            List<ImageTemplate> imageTemplates = JsonConvert.DeserializeObject<List<ImageTemplate>>(dataText);

            // TODO: finish tomorrow
        }
    }
}
