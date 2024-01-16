using System;
using System.Collections.Generic;

namespace Assignment_08.Models
{
    public partial class Book
    {
        public Book()
        {
            Authors = new HashSet<Author>();
        }

        public int BookId { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public int? CategoryId { get; set; }
        public int? PublisherId { get; set; }

        public virtual BookCategory? Category { get; set; }
        public virtual Publisher? Publisher { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
