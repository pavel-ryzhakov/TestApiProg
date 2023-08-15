using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestApiProg.Models;


namespace TestApiProg.Data
{
    public class TestAPIContext : DbContext
    {
        public TestAPIContext (DbContextOptions<TestAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
