using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Models
{
    public class DbInitilizer
    {

        public static void InIt(BookContext dBContext)
        {
            dBContext.Database.EnsureCreated();
        }
    }
}
