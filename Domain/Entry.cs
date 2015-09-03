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
            Date = DateTime.Now;
        }

        protected Entry()
        {
        }

        public string Title { get; set; }

        public string Body { get; set; }

        public int EntryId { get; set; }

        public Blog Blog { get; set; }

        public int BlogId { get; set; }

        public DateTime Date { get; set; }
    }
}