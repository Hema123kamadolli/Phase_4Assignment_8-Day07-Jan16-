﻿using System;
using System.Collections.Generic;

namespace Assignment_08.Models
{
    public partial class BookCategory
    {
        public BookCategory()
        {
            Books = new HashSet<Book>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
