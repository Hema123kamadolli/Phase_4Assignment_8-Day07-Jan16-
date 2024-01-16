using System;
using System.Collections.Generic;

namespace Assignment_08.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int PublisherId { get; set; }
        public string? PublisherName { get; set; }
        public string? PublisherAddress { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
