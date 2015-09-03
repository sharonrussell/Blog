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
    public class EntryRepositoryTests
    {
        private IEntryRepository _entryRepository;

        private readonly Mock<IContextFactory> _contextFactory = new Mock<IContextFactory>();
        private readonly Mock<BlogContext> _context = new Mock<BlogContext>();

        private readonly Blog _blog = new Blog("Sharon") {BlogId = 1};
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

            _entryRepository = new EntryRepository(_contextFactory.Object);
        }

        [Test]
        public void When_AddingEntry_Should_AddToDB()
        {
            _entryRepository.AddEntry(_blog.BlogId, "title", "body");

            _context.Verify(o => o.SaveChanges(), Times.Once);
        }

        [Test]
        public void When_AddingEntry_WithBlankTitle_Should_Error()
        {
            Assert.Throws<ArgumentNullException>(() => _entryRepository.AddEntry(It.IsAny<int>(), "", "body"));
        }

        [Test]
        public void When_AddingEntry_WithBlankBody_Should_Error()
        {
            Assert.Throws<ArgumentNullException>(() => _entryRepository.AddEntry(It.IsAny<int>(), "title", ""));
        }

        [Test]
        public void When_AddingEntry_AndCannotFindBlog_Should_Error_AndNotSaveToDB()
        {
            _blogs = new List<Blog>().AsQueryable();

            _context.Verify(o => o.SaveChanges(), Times.Never);
            Assert.Throws<ObjectDoesNotExistException>(() =>_entryRepository.AddEntry(It.IsAny<int>(), "title", "body"));
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
