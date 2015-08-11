using System;
using System.ServiceModel;
using DataAccess.Exceptions;
using DataAccess.Repository;

namespace Services
{
    public class EntryService : IEntryService
    {
        private readonly IBlogRepository _blogRepository;

        public EntryService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public void AddEntry(Guid blogId, string title, string body)
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
                _blogRepository.AddEntry(blogId, title, body);
            }
            catch (ObjectDoesNotExistException ex)
            {
                throw new FaultException<ObjectDoesNotExistException>(ex);
            }
        }

        public void RemoveEntry(Guid blogId, Guid entryId)
        {
            try
            {
                _blogRepository.RemoveEntry(blogId, entryId);
            }
            catch (ObjectDoesNotExistException ex)
            {
                throw new FaultException<ObjectDoesNotExistException>(ex);
            }
        }
    }
}
