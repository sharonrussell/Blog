using System;
using System.ServiceModel;
using DataAccess;
using DataAccess.Exceptions;
using DataAccess.Repository;
using Moq;
using NUnit.Framework;

namespace Services.Tests
{
    [TestFixture]
    public class BlogServiceTests
    {
        private Mock<IBlogRepository> _blogRepository;

        private IBlogService _blogService;

        [SetUp]
        public void SetUp()
        {
            _blogRepository = new Mock<IBlogRepository>();

            _blogService = new BlogService(_blogRepository.Object);
        }
       
        [Test]
        public void When_AddingEntry_Should_AddToBlogRepository()
        {
            _blogService.AddEntry(Guid.NewGuid(), "title", "body");

            _blogRepository.Verify(o => o.AddEntry(It.IsAny<Guid>(), "title", "body"), Times.Once);
        }

        [Test]
        public void When_AddingEntryWithNullTitle_Should_Error()
        {
            Assert.Throws<ArgumentNullException>(() => _blogService.AddEntry(It.IsAny<Guid>(), "", "body"));
        }

        [Test]
        public void When_AddingEntryWithNullBody_Should_Error()
        {
            Assert.Throws<ArgumentNullException>(() => _blogService.AddEntry(It.IsAny<Guid>(), "title", ""));
        }

        [Test]
        public void When_AddingEntryToBlogThatCantBeFound_ShouldError()
        {
            _blogRepository.Setup(o => o.AddEntry(It.IsAny<Guid>(), "title", "body")).Throws<ObjectDoesNotExistException>();

            Assert.Throws<FaultException<ObjectDoesNotExistException>>(() => _blogService.AddEntry(It.IsAny<Guid>(), "title", "body"));
        }
    }
}
