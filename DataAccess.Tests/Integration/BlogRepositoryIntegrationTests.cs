using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Context;
using DataAccess.Repository;
using Domain;
using Moq;
using NUnit.Framework;

namespace DataAccess.Tests.Integration
{
    [TestFixture]
    public class BlogRepositoryIntegrationTests
    {
        private readonly Mock<IContextFactory> _contextFactory = new Mock<IContextFactory>();

        private IBlogRepository _repository;
 
        private Blog _blog = new Blog("Sharon");

        private TestBlogContext _context;

        [SetUp]
        public void SetUp()
        {
            _repository = new BlogRepository(_contextFactory.Object);
            _contextFactory.Setup(o => o.CreateContext()).Returns(new TestBlogContext());
        }

        [Test]
        public void When_AddingEntry_Should_SaveToDB()
        {
            using (_context = new TestBlogContext())
            {
                _context.Blogs.Add(_blog);
                _context.SaveChanges();
            }

            _repository.AddEntry(_blog.BlogId, "title", "body");

            using (_context = new TestBlogContext())
            {
                _blog = _context.Blogs.Include(b => b.Entries).Single(b => b.BlogId == _blog.BlogId);

                Assert.That(_blog.Entries.Count, Is.EqualTo(1));
                Assert.That(_blog.Entries.First().Title, Is.EqualTo("title"));
            }
        }

        [Test]
        public void When_GettingBlogs_Should_GetFromDB()
        {
            Blog blog = new Blog("Sharon");

            Entry entry = new Entry("title", "body");

            using (_context = new TestBlogContext())
            {
                _context.Blogs.Add(blog);
                _context.SaveChanges();

                blog.AddEntry(entry);
                _context.Entries.Add(entry);

                _context.SaveChanges();
            }

            IList<Blog> blogs = _repository.GetBlogs();
            Blog dbBlog = blogs.Single(b => b.BlogId == blog.BlogId);

            Assert.That(blogs, Is.Not.Null);
            Assert.That(blogs.Count(), Is.GreaterThan(0));
            Assert.That(dbBlog, Is.Not.Null);
            Assert.That(dbBlog.Entries.Count(), Is.EqualTo(1));
            Assert.That(dbBlog.Entries.FirstOrDefault(o => o.EntryId == entry.EntryId), Is.Not.Null);
        }
    }
}
