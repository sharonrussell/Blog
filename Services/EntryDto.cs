using System;
using System.Runtime.Serialization;

namespace Services
{
    [DataContract]
    public class EntryDto
    {
        [DataMember]
        public Guid BlogId { get; set; }

        [DataMember]
        public Guid EntryId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public DateTime Date { get; set; }
    }
}