using System.Collections.Generic;

namespace Web.Models
{
    public class EntryViewModels
    {
        public string Author { get; set; }

        public int BlogId { get; set; }

        public IEnumerable<EntryViewModel> Entries { get; set; }
    }
}