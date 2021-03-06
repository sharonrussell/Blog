﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using DataAccess.Context;
using DataAccess.Repository;
using Domain;
using Moq;
using NUnit.Framework;

namespace DataAccess.Tests.Integration
{
    [TestFixture]
    public class EntryRepositoryIntegrationTests
    {
        private readonly Mock<IContextFactory> _contextFactory = new Mock<IContextFactory>();

        private IEntryRepository _repository;
 
        private Blog _blog = new Blog("Sharon");

        private TestBlogContext _context;

        [SetUp]
        public void SetUp()
        {
            _repository = new EntryRepository(_contextFactory.Object);
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
        public void When_RemovingEntry_Should_RemoveFromBlog()
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

            _repository.RemoveEntry(blog.BlogId, entry.EntryId);

            using (_context = new TestBlogContext())
            {
                _blog = _context.Blogs.Single(b => b.BlogId == _blog.BlogId);

                Assert.That(_blog.Entries.Count, Is.EqualTo(0));
            }
        }

        [Test]
        public void When_GettingEntries_Should_GetFromDB()
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

            IEnumerable<Entry> entries = _repository.GetEntries(blog.BlogId);

            Assert.That(entries.Count(), Is.EqualTo(1));
        }

        [Test]
        public void When_GettingEntry_Should_GetFromDB()
        {
            Blog blog = new Blog("Sharon");

            Entry entry = new Entry("title", "body");

            blog.AddEntry(entry);
            using (_context = new TestBlogContext())
            {
                _context.Blogs.Add(blog);
                _context.Entries.Add(entry);
                _context.SaveChanges();
            }

            entry = _repository.GetEntry(entry.EntryId);

            Assert.That(entry, Is.Not.Null);
            Assert.That(entry.Title, Is.EqualTo("title"));
        }

        [Test]
        public void When_EditingEntry_Should_SaveToDB()
        {
            Blog blog = new Blog("Sharon");

            Entry entry = new Entry("title", "body");

            blog.AddEntry(entry);
            using (_context = new TestBlogContext())
            {
                _context.Blogs.Add(blog);
                _context.Entries.Add(entry);
                _context.SaveChanges();
            }

            _repository.EditEntry(entry.EntryId, "new title", "new body");

            using (_context = new TestBlogContext())
            {
                entry = _context.Entries.Single(e => e.EntryId == entry.EntryId);
            }

            Assert.That(entry, Is.Not.Null);
            Assert.That(entry.Title, Is.EqualTo("new title"));
            Assert.That(entry.Body, Is.EqualTo("new body"));
        }
    }
}
