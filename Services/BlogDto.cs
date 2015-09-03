using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Services
{
    [DataContract]
    public class BlogDto
    {
        [DataMember]
        public string Author { get;set; }

        [DataMember]
        public IEnumerable<EntryDto> Entries { get; set; }

        [DataMember]
        public int BlogId { get; set; }
    }
}