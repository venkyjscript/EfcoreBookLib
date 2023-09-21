using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Models
{
    public class EmpContext : DbContext
    {
        public EmpContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<employees> Employees { get; set; }
        public DbSet<dept_emp> Depts { get; set; }
    }
}
