using System;
using System.ServiceModel;
using DataAccess.Exceptions;
using DataAccess.Repository;
using Domain;

namespace Services
{
    public class EntryService : IEntryService
    {
        private readonly IEntryRepository _entryRepository;

        public EntryService(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public EntryService()
        {
        }

        public void AddEntry(int blogId, string title, string body)
        {
            if (String.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("title");
            }

            if (String.IsNullOrWhiteSpace(body))
            {
                throw new ArgumentNullException("body");
            }

            try
            {
                _entryRepository.AddEntry(blogId, title, body);
            }
            catch (ObjectDoesNotExistException ex)
            {
                throw new FaultException<ObjectDoesNotExistException>(ex);
            }
        }

        public void RemoveEntry(int blogId, int entryId)
        {
            try
            {
                _entryRepository.RemoveEntry(blogId, entryId);
            }
            catch (ObjectDoesNotExistException ex)
            {
                throw new FaultException<ObjectDoesNotExistException>(ex);
            }
        }

        public void EditEntry(EntryDto entryDto)
        {
            try
            {
                _entryRepository.EditEntry(entryDto.EntryId, entryDto.Title, entryDto.Body);
            }
            catch (ObjectDoesNotExistException ex)
            {
                throw new FaultException<ObjectDoesNotExistException>(ex);
            }
        }

        public EntryDto GetEntry(int entryId)
        {
            Entry entry;
            
            try
            {
                entry = _entryRepository.GetEntry(entryId);
            }
            catch(ObjectDoesNotExistException ex)
            {
                throw new FaultException<ObjectDoesNotExistException>(ex);
            }

            EntryDto entryDto = new EntryDto
            {
                BlogId = entry.BlogId,
                EntryId = entry.EntryId,
                Body = entry.Body,
                Title = entry.Title,
                Date = entry.Date
            };

            return entryDto;
        }
    }
}
