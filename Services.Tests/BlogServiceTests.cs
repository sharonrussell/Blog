using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using DataAccess.Exceptions;
using DataAccess.Repository;
using Domain;
using Moq;
using NUnit.Framework;

namespace Services.Tests
{
    [TestFixture]
    public class BlogServiceTests
    {
        private Mock<IBlogRepository> _blogRepository;

        private IBlogService _blogService;

        private Blog _blog;

        private readonly List<Blog> _blogs = new List<Blog>();
        
        [SetUp]
        public void SetUp()
        {
            _blogRepository = new Mock<IBlogRepository>();

            _blogService = new BlogService(_blogRepository.Object);

            _blog = new Blog("Sharon");
            _blog.AddEntry(new Entry("title", "body"));

            _blogs.Add(_blog);
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
        public void When_GettingBlogs_Should_ReturnBlogs()
        {
            _blogRepository.Setup(o => o.GetBlogs()).Returns(_blogs);

            IEnumerable<BlogDto> blogs = _blogService.GetBlogs().ToList();
            BlogDto blog = blogs.Single(o => o.BlogId == _blog.BlogId);

            Assert.That(blogs, Is.Not.Null);
            Assert.That(blogs.Count(), Is.EqualTo(1));
            Assert.That(blog.Author == "Sharon");
            Assert.That(blog.Entries, Is.Not.Null);
            Assert.That(blog.Entries.FirstOrDefault(o => o.Body == "body"), Is.Not.Null);
        }
    }
}
