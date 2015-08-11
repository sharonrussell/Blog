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
            _entryService.AddEntry(Guid.NewGuid(), "title", "body");

            _entryRepository.Verify(o => o.AddEntry(It.IsAny<Guid>(), "title", "body"), Times.Once);
        }

        [Test]
        public void When_AddingEntryWithNullTitle_Should_Error()
        {
            Assert.Throws<ArgumentNullException>(() => _entryService.AddEntry(It.IsAny<Guid>(), "", "body"));
        }

        [Test]
        public void When_AddingEntryWithNullBody_Should_Error()
        {
            Assert.Throws<ArgumentNullException>(() => _entryService.AddEntry(It.IsAny<Guid>(), "title", ""));
        }

        [Test]
        public void When_AddingEntryToBlogThatCantBeFound_ShouldError()
        {
            _entryRepository.Setup(o => o.AddEntry(It.IsAny<Guid>(), "title", "body")).Throws<ObjectDoesNotExistException>();

            Assert.Throws<FaultException<ObjectDoesNotExistException>>(() => _entryService.AddEntry(It.IsAny<Guid>(), "title", "body"));
        }

        [Test]
        public void When_RemovingEntry_Should_RemoveEntry()
        {
            _entryService.RemoveEntry(It.IsAny<Guid>(), It.IsAny<Guid>());

            _entryRepository.Verify(o => o.RemoveEntry(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void When_RemovingEntry_AndCannotFindBlogOrEntry_Should_Error()
        {
            _entryRepository.Setup(o => o.RemoveEntry(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Throws<ObjectDoesNotExistException>();

            Assert.Throws<FaultException<ObjectDoesNotExistException>>(
                () => _entryService.RemoveEntry(It.IsAny<Guid>(), It.IsAny<Guid>()));
        }
    }
}