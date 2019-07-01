using MemeGenerator.DAL;
using MemeGenerator.DAL.InitialMemesProvider;
using MemeGenerator.Domain.Models;
using System.Collections.Generic;

namespace MemeGenerator.Tests
{
    public class StubInitialMemesProvider : IInitialMemesProvider
    {
        public ImageTemplateList GetDataFromJson()
        {
            var fakeImageTemplates = new ImageTemplateList
            {
                Folder = "folder",
                ImageTemplate = new List<ImageTemplate>
                {
                    new ImageTemplate()
                    {
                        Name = "name",
                        Folder = "folder",
                        Description = "description",
                    }
                }
            };

            return fakeImageTemplates;
        }
    }
}
