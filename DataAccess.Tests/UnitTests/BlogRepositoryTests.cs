using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Context;
using DataAccess.Repository;
using Domain;
using Moq;
using NUnit.Framework;

namespace DataAccess.Tests.UnitTests
{
    public class BlogRepositoryTests
    {
        private IBlogRepository _blogRepository;

        private readonly Mock<IContextFactory> _contextFactory = new Mock<IContextFactory>();
        private readonly Mock<BlogContext> _context = new Mock<BlogContext>();

        private readonly Blog _blog = new Blog("Sharon");
        private readonly Mock<DbSet<Blog>> _blogSet = new Mock<DbSet<Blog>>();

        private IQueryable<Blog> _blogs;

        [SetUp]
        public void SetUp()
        {
            _blogs = new List<Blog> { _blog }.AsQueryable();

            MockDbSets();

            _contextFactory.Setup(o => o.CreateContext()).Returns(_context.Object);

            _blogRepository = new BlogRepository(_contextFactory.Object);
        }

        [Test]
        public void When_AddingBlog_Should_AddToListOfBlogs()
        {
            Blog blog = new Blog("Sharon2");
            _blogRepository.AddBlog(blog);

            _context.Verify(o => o.SaveChanges(), Times.Once);
        }

        [Test]
        public void When_AddingNullBlog_Should_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _blogRepository.AddBlog(null));
        } 

        private void MockDbSets()
        {
            _blogSet.As<IQueryable<Blog>>().Setup(o => o.Provider).Returns(_blogs.Provider);
            _blogSet.As<IQueryable<Blog>>().Setup(o => o.Expression).Returns(_blogs.Expression);
            _blogSet.As<IQueryable<Blog>>().Setup(o => o.ElementType).Returns(_blogs.ElementType);
            _blogSet.As<IQueryable<Blog>>().Setup(o => o.GetEnumerator()).Returns(_blogs.GetEnumerator);

            _context.Setup(o => o.Blogs).Returns(_blogSet.Object);
        }
    }
}
