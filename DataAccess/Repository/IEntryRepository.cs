using System;

namespace DataAccess.Repository
{
    public interface IEntryRepository
    {
        void AddEntry(Guid blogId, string title, string body);

        void RemoveEntry(Guid blogId, Guid entryId);
    }
}
