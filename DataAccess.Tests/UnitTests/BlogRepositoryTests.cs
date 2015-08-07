using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Context;
using DataAccess.Exceptions;
using DataAccess.Repository;
using Domain;
using Moq;
using NUnit.Framework;

namespace DataAccess.Tests.UnitTests
{
    [TestFixture]
    public class BlogRepositoryTests
    {
        private IBlogRepository _blogRepository;

        private readonly Mock<IContextFactory> _contextFactory = new Mock<IContextFactory>();
        private readonly Mock<BlogContext> _context = new Mock<BlogContext>(); 

        private readonly Blog _blog = new Blog("Sharon");
        private readonly Mock<DbSet<Blog>> _blogSet = new Mock<DbSet<Blog>>();
        private readonly Mock<DbSet<Entry>> _entrySet = new Mock<DbSet<Entry>>();

        private readonly IQueryable<Entry> _entries = new List<Entry>().AsQueryable();
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
        public void When_AddingEntry_Should_AddToDB()
        {
            _blogRepository.AddEntry(_blog.BlogId, "title", "body");

            _context.Verify(o => o.SaveChanges(), Times.Once);
        }

        [Test]
        public void When_AddingEntry_WithBlankTitle_Should_Error()
        {
            Assert.Throws<ArgumentNullException>(() => _blogRepository.AddEntry(It.IsAny<Guid>(), "", "body"));
        }

        [Test]
        public void When_AddingEntry_WithBlankBody_Should_Error()
        {
            Assert.Throws<ArgumentNullException>(() => _blogRepository.AddEntry(It.IsAny<Guid>(), "title", ""));
        }

        [Test]
        public void When_AddingEntry_AndCannotFindBlog_Should_Error_AndNotSaveToDB()
        {
            _blogs = new List<Blog>().AsQueryable();

            _context.Verify(o => o.SaveChanges(), Times.Never);
            Assert.Throws<ObjectDoesNotExistException>(() =>_blogRepository.AddEntry(It.IsAny<Guid>(), "title", "body"));
        }

        private void MockDbSets()
        {
            _blogSet.As<IQueryable<Blog>>().Setup(o => o.Provider).Returns(_blogs.Provider);
            _blogSet.As<IQueryable<Blog>>().Setup(o => o.Expression).Returns(_blogs.Expression);
            _blogSet.As<IQueryable<Blog>>().Setup(o => o.ElementType).Returns(_blogs.ElementType);
            _blogSet.As<IQueryable<Blog>>().Setup(o => o.GetEnumerator()).Returns(_blogs.GetEnumerator);

            _entrySet.As<IQueryable<Entry>>().Setup(o => o.Provider).Returns(_entries.Provider);
            _entrySet.As<IQueryable<Entry>>().Setup(o => o.Expression).Returns(_entries.Expression);
            _entrySet.As<IQueryable<Entry>>().Setup(o => o.ElementType).Returns(_entries.ElementType);
            _entrySet.As<IQueryable<Entry>>().Setup(o => o.GetEnumerator()).Returns(_entries.GetEnumerator);

            _context.Setup(o => o.Blogs).Returns(_blogSet.Object);
            _context.Setup(o => o.Entries).Returns(_entrySet.Object);
        }
    }
}
