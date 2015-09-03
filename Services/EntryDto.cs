using System;
using System.Runtime.Serialization;

namespace Services
{
    [DataContract]
    public class EntryDto
    {
        [DataMember]
        public int BlogId { get; set; }

        [DataMember]
        public int EntryId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public DateTime Date { get; set; }
    }
}