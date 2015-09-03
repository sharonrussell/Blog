using System;
using System.ServiceModel;
using DataAccess.Exceptions;
using DataAccess.Repository;
using Moq;
using NUnit.Framework;

namespace Services.Tests
{
    [TestFixture]
    public class EntryServiceTests
    {
        private Mock<IEntryRepository> _entryRepository;

        private IEntryService _entryService;

        [SetUp]
        public void SetUp()
        {
            _entryRepository = new Mock<IEntryRepository>();

            _entryService = new EntryService(_entryRepository.Object);
        }

        [Test]
        public void When_AddingEntry_Should_AddToBlogRepository()
        {
            _entryService.AddEntry(It.IsAny<int>(), "title", "body");

            _entryRepository.Verify(o => o.AddEntry(It.IsAny<int>(), "title", "body"), Times.Once);
        }

        [Test]
        public void When_AddingEntryWithNullTitle_Should_Error()
        {
            Assert.Throws<ArgumentNullException>(() => _entryService.AddEntry(It.IsAny<int>(), "", "body"));
        }

        [Test]
        public void When_AddingEntryWithNullBody_Should_Error()
        {
            Assert.Throws<ArgumentNullException>(() => _entryService.AddEntry(It.IsAny<int>(), "title", ""));
        }

        [Test]
        public void When_AddingEntryToBlogThatCantBeFound_ShouldError()
        {
            _entryRepository.Setup(o => o.AddEntry(It.IsAny<int>(), "title", "body")).Throws<ObjectDoesNotExistException>();

            Assert.Throws<FaultException<ObjectDoesNotExistException>>(() => _entryService.AddEntry(It.IsAny<int>(), "title", "body"));
        }

        [Test]
        public void When_RemovingEntry_Should_RemoveEntry()
        {
            _entryService.RemoveEntry(It.IsAny<int>(), It.IsAny<int>());

            _entryRepository.Verify(o => o.RemoveEntry(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void When_RemovingEntry_AndCannotFindBlogOrEntry_Should_Error()
        {
            _entryRepository.Setup(o => o.RemoveEntry(It.IsAny<int>(), It.IsAny<int>()))
                .Throws<ObjectDoesNotExistException>();

            Assert.Throws<FaultException<ObjectDoesNotExistException>>(
                () => _entryService.RemoveEntry(It.IsAny<int>(), It.IsAny<int>()));
        }
    }
}