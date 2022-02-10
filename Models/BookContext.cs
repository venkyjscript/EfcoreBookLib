﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Models
{
    public class BookContext:DbContext
    {
        public BookContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}

