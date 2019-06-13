using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeGenerator.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MemeGenerator
{
    public class DbInitializer
    {
        private readonly ImageTemplate imageTemplate;

        public DbInitializer(IOptionsMonitor<ImageTemplate> imageTemplateAccessor)
        {
            imageTemplate = imageTemplateAccessor.CurrentValue;
        }
    }
}
