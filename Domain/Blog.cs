using System;
using System.Collections.Generic;

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
            BlogId = Guid.NewGuid();
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

            entry.BlogId = BlogId;
            _entries.Add(entry);
        }

        public Guid BlogId { get; private set; }

        public string Author { get; private set; }
    }
}
