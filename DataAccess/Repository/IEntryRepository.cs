﻿using System.Collections.Generic;
using Domain;

namespace DataAccess.Repository
{
    public interface IEntryRepository
    {
        void AddEntry(int blogId, string title, string body);

        void RemoveEntry(int blogId, int entryId);

        IEnumerable<Entry> GetEntries(int blogId);
        
        Entry GetEntry(int entryId);
        
        void EditEntry(int entryId, string title, string body);
    }
}
