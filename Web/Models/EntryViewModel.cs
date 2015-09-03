using System;

namespace Web.Models
{
    public class EntryViewModel
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public int EntryId { get; set; }

        public int BlogId { get; set; }

        public DateTime Date { get; set; }
    }
}