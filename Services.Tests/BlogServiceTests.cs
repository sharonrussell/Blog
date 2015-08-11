using System.Collections.Generic;
using System.Linq;
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
        private Mock<IEntryRepository> _entryRepository;

        [SetUp]
        public void SetUp()
        {
            _blogRepository = new Mock<IBlogRepository>();
            _entryRepository = new Mock<IEntryRepository>();

            _blogService = new BlogService(_blogRepository.Object, _entryRepository.Object);

            _blog = new Blog("Sharon");
            _blog.AddEntry(new Entry("title", "body"));

            _blogs.Add(_blog);
            _blogRepository.Setup(o => o.GetBlogs()).Returns(_blogs);
        }

        [Test]
        public void When_GettingBlogs_Should_ReturnBlogs()
        {
            IEnumerable<BlogDto> blogs = _blogService.GetBlogs().ToList();
            BlogDto blog = blogs.Single(o => o.BlogId == _blog.BlogId);

            Assert.That(blogs, Is.Not.Null);
            Assert.That(blog.Author == "Sharon");
            Assert.That(blog.Entries.Count(), Is.EqualTo(1));
        }

        [Test]
        public void When_AddingBlog_Should_AddToBlogs()
        {
            _blogService.AddBlog(new BlogDto
            {
                Author = "Sharon"
            });

            _blogRepository.Verify(o => o.AddBlog(It.IsAny<Blog>()), Times.Once);
        }
    }
}
