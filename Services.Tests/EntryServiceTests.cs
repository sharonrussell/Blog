using System;
using System.Collections.Generic;
using System.ServiceModel;
using DataAccess.Exceptions;
using DataAccess.Repository;
using Domain;
using Moq;
using NUnit.Framework;

namespace Services.Tests
{
    [TestFixture]
    public class EntryServiceTests
    {
        private Mock<IBlogRepository> _blogRepository;

        private IEntryService _blogService;

        private Blog _blog;

        private readonly List<Blog> _blogs = new List<Blog>();

        [SetUp]
        public void SetUp()
        {
            _blogRepository = new Mock<IBlogRepository>();

            _blogService = new EntryService(_blogRepository.Object);

            _blog = new Blog("Sharon");
            _blog.AddEntry(new Entry("title", "body"));

            _blogs.Add(_blog);
            _blogRepository.Setup(o => o.GetBlogs()).Returns(_blogs);
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

        [Test]
        public void When_RemovingEntry_Should_RemoveEntry()
        {
            _blogService.RemoveEntry(It.IsAny<Guid>(), It.IsAny<Guid>());

            _blogRepository.Verify(o => o.RemoveEntry(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void When_RemovingEntry_AndCannotFindBlogOrEntry_Should_Error()
        {
            _blogRepository.Setup(o => o.RemoveEntry(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Throws<ObjectDoesNotExistException>();

            Assert.Throws<FaultException<ObjectDoesNotExistException>>(
                () => _blogService.RemoveEntry(It.IsAny<Guid>(), It.IsAny<Guid>()));
        }
    }
}