using System.Data.Entity;
using System.Linq;
using Domain;
using NUnit.Framework;

namespace DataAccess.Tests.Integration
{
    [TestFixture]
    public class BlogContextIntegrationTests
    {
        TestBlogContext _context;

        [SetUp]
        public void SetUp()
        {
            Database.SetInitializer(new TestInitializer());

            _context = new TestBlogContext();
        }

        [Test]
        public void When_CreatingBlog_Should_SaveToDB()
        {
            Blog blog = new Blog("Sharon");

            using (_context = new TestBlogContext())
            {
                _context.Blogs.Add(blog);
                _context.SaveChanges();
            }

            using (_context = new TestBlogContext())
            {
                Blog dbBlog = _context.Blogs.Single(b => b.BlogId == blog.BlogId);
                Assert.That(dbBlog, Is.Not.Null);
                Assert.That(dbBlog.Author, Is.EqualTo("Sharon"));
            }
        }

        [Test]
        public void When_AddingEntryToBlog_Should_SaveToDB()
        {
            Blog blog = new Blog("Sharon");
            Entry entry = new Entry("title", "body");

            using (_context = new TestBlogContext())
            {
                _context.Blogs.Add(blog);
                blog.AddEntry(entry);

                _context.Entries.Add(entry);
                _context.SaveChanges();
            }

            using (_context = new TestBlogContext())
            {
                blog = _context.Blogs.Include(b => b.Entries).Single(b => b.BlogId == blog.BlogId);

                entry = blog.Entries.Single(e => e.EntryId == entry.EntryId);

                Assert.That(entry, Is.Not.Null);
                Assert.That(entry.Title, Is.EqualTo("title"));
                Assert.That(entry.Body, Is.EqualTo("body"));
            }
        }
    }
}
