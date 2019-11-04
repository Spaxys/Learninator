using Learninator.Models;
using Learninator.Repositories;
using Learninator.Services;
using Learninator.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Learninator.Tests.Services
{
    public class LinksServiceTests
    {
        private Mock<ITagsRepository> tagsRepo;
        private Mock<ILinksRepository> linksRepo;
        public LinksServiceTests()
        {
            tagsRepo = new Mock<ITagsRepository>();
            linksRepo = new Mock<ILinksRepository>();
        }


        [Fact]
        public void CreateLinkCallsLinkRepoCreate()
        {
            //Arrange
            var linksService = new LinksService(tagsRepo.Object, linksRepo.Object);

            //Act
            int id = linksService.CreateLinkWithTags(LinksServiceObject.GetLink());

            //Assert
            linksRepo.Verify(x => x.CreateLink(It.IsAny<Link>()), Times.Once);
        }

        [Fact]
        public void CreateLinkCallsLinkAndTagsCreate()
        {
            //Arrange
            var linksService = new LinksService(tagsRepo.Object, linksRepo.Object);
            var link = LinksServiceObject
                .GetLink()
                .WithTags();
            //Act
            int id = linksService.CreateLinkWithTags(link);

            //Assert
            tagsRepo.Verify(x => x.SaveTagsOnLink(It.IsAny<List<Tag>>(), 
                                                  It.IsAny<int>()), 
                                                  Times.Once);
        }
        [Fact]
        public void CreateLinkReturnsValidId()
        {
            //Arrange
            linksRepo.Setup(x => x.CreateLink(It.IsAny<Link>()))
                .Returns(1);
            var linksService = new LinksService(tagsRepo.Object, linksRepo.Object);
            var link = LinksServiceObject
                .GetLink()
                .WithTags();
            //Act
            int id = linksService.CreateLinkWithTags(link);

            //Assert
            Assert.True(id > 0);
        }

    }
}
