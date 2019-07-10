using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MemeGenerator.BLL.Services.InitialMemesPopulator;
using MemeGenerator.DAL;
using MemeGenerator.DAL.MigrationsChecker;
using MemeGenerator.DAL.Providers;
using MemeGenerator.DAL.Repositories;
using MemeGenerator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace MemeGenerator.Tests.Services
{
    [TestFixture]
    public class InitialMemesPopulatorTests
    {
        private Mock<IInitialMemesProvider> mockInitialMemesProvider;
        private Mock<IImageTemplateRepository> mockRepository;
        private Mock<IMigrationsChecker> mockMigrationsChecker;

        [SetUp]
        public void Setup()
        {
            var image = new Task<ImageTemplateList>(() => new ImageTemplateList
            {
                Folder = "folder",
                ImageTemplate = new List<ImageTemplate>
                {
                    new ImageTemplate
                    {
                        Name = "name",
                        Data = new byte[0],
                        Description = "description"
                    }
                }
            });
            mockInitialMemesProvider = new Mock<IInitialMemesProvider>();
            mockInitialMemesProvider.Setup(m => m.GetData()).Returns(image);

            IEnumerable<ImageTemplate> emptyImageList = new List<ImageTemplate> { };
            mockRepository = new Mock<IImageTemplateRepository>();
            mockRepository.Setup(m => m.GetAll()).Returns(emptyImageList);

            mockMigrationsChecker = new Mock<IMigrationsChecker>();
            mockMigrationsChecker.Setup(m => m.AreAllMigrationsApplied()).Returns(true);
        }

        [Test]
        public void Initialize_AllMigrationsApplyAndImageTableEmpty_filledDb()
        {
            var initialMemesPopulator = new InitialMemesPopulator(
                mockInitialMemesProvider.Object,
                mockRepository.Object,
                mockMigrationsChecker.Object);

            initialMemesPopulator.Initialize();

            mockInitialMemesProvider.Verify(mock => mock.GetData(), Times.Once());
        }

        //[Test]
        //public void Initialize_NotAllMigrationsAreApplied_DbNotFilled()
        //{
        //    var mockMigrationsChecker = new Mock<IMigrationsChecker>();
        //    mockMigrationsChecker.Setup(m => m.AreAllMigrationsApplied()).Returns(false);

        //    using (var context = new MemeGeneratorDbContext(options))
        //    {
        //        var mockRepository = new ImageTemplateRepository(context);

        //        InitialMemesPopulator initialMemesPopulator =
        //            new InitialMemesPopulator(mockInitialMemesProvider.Object, mockRepository, mockMigrationsChecker.Object);

        //        initialMemesPopulator.Initialize();

        //        context.ImageTemplates.Count().Should().Be(0);
        //    }
        //}

        //[Test]
        //public void Initialize_ImageTableNotEmpty_DbNotChanged()
        //{
        //    var mockMigrationsChecker = new Mock<IMigrationsChecker>();
        //    mockMigrationsChecker.Setup(m => m.AreAllMigrationsApplied()).Returns(true);

        //    using (var context = new MemeGeneratorDbContext(options))
        //    {
        //        var mockRepository = new ImageTemplateRepository(context);

        //        mockRepository.Insert(new ImageTemplate());
        //        mockRepository.Save();

        //        InitialMemesPopulator initialMemesPopulator =
        //            new InitialMemesPopulator(mockInitialMemesProvider.Object, mockRepository, mockMigrationsChecker.Object);

        //        context.ImageTemplates.Count().Should().Be(1);

        //        initialMemesPopulator.Initialize();

        //        context.ImageTemplates.Count().Should().Be(1);
        //    }
        //}
    }
}