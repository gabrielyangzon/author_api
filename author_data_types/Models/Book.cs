﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace author_data_types.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; } = string.Empty;

    }

}