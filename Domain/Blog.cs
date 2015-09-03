using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Blog
    {
        private readonly List<Entry> _entries;

        public Blog(string author) : this()
        {
            Author = author;
        }

        protected Blog()
        {
            _entries = new List<Entry>();
        }

        public ICollection<Entry> Entries
        {
            get { return _entries; }
        }

        public void AddEntry(Entry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }

            _entries.Add(entry);
            entry.BlogId = BlogId;
        }

        public int BlogId { get; set; }

        public string Author { get; private set; }

        public void RemoveEntry(int entryId)
        {
            Entry entry = _entries.SingleOrDefault(e => e.EntryId == entryId);
            
            if (entry == null)
            {
                throw new ArgumentException("Could not find entry with id " + entryId);
            }

            _entries.Remove(entry);
        }
    }
}
