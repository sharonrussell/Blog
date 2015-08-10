using System;
using System.Linq;
using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class BlogTests
    {
        private Blog _blog;

        [SetUp]
        public void SetUp()
        {
            _blog = new Blog("Sharon");
        }

        [Test]
        public void When_AddingEntry_Should_AddToEntries()
        {
            Entry entry = new Entry("title", "body");

            _blog.AddEntry(entry);

            Assert.That(_blog.Entries.Count(), Is.EqualTo(1));
            Assert.That(_blog.Entries.FirstOrDefault(), Is.Not.Null);
            Assert.That(_blog.Entries.FirstOrDefault(), Is.EqualTo(entry));
        }

        [Test]
        public void When_AddingNullEntry_Should_Error()
        {
            Assert.Throws<ArgumentNullException>(() => _blog.AddEntry(null));
        }

        [Test]
        public void When_RemovingEntry_Should_RemoveFromEntries()
        {
            Entry entry = new Entry("title", "body");

            _blog.AddEntry(entry);

            _blog.RemoveEntry(entry.EntryId);

            Assert.That(_blog.Entries.Count, Is.EqualTo(0));
        }

        [Test]
        public void When_RemovingEntry_ThatCannotBeFound_ShouldError()
        {
            Assert.Throws<ArgumentException>(() => _blog.RemoveEntry(Guid.NewGuid()));
        }
    }
}
