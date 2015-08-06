using System;

namespace Domain
{
    public class Entry
    {
        public Entry(string title, string body)
        {
            if (String.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("title");
            }

            if (String.IsNullOrWhiteSpace(body))
            {
                throw new ArgumentNullException("body");
            }

            Title = title;
            Body = body;
            EntryId = Guid.NewGuid();
        }

        protected Entry()
        {
        }

        public string Title { get; set; }

        public string Body { get; set; }

        public Guid EntryId { get; private set; }

        public Blog Blog { get; set; }

        public Guid BlogId { get; set; }
    }
}