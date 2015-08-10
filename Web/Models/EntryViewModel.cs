using System;

namespace Web.Models
{
    public class EntryViewModel
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public Guid EntryId { get; set; }

        public Guid BlogId { get; set; }
    }
}