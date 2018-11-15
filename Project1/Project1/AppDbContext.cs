using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) 
            :base(options)
        {

        }
        public DbSet <Customer> Customers { get; set; }
    }
}
