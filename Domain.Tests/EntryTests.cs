﻿using System;
using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class EntryTests
    {
        Entry _entry;

        [SetUp]
        public void SetUp()
        {
            _entry = new Entry("title", "body");
        }

        [Test]
        public void When_GettingTitle_Should_Return_Title()
        {
            Assert.That(_entry.Title == "title");
        }

        [Test]
        public void When_GettingBody_Should_Return_Body()
        {
            Assert.That(_entry.Body == "body");
        }

        [Test]
        public void When_EditingEntry_WithEmptyTitle_ShouldError()
        {
            Assert.Throws<ArgumentNullException>(() => _entry.EditEntry("", "body"));
        }        
        
        [Test]
        public void When_EditingEntry_WithEmptyBody_ShouldError()
        {
            Assert.Throws<ArgumentNullException>(() => _entry.EditEntry("title", ""));
        }
    }
}
