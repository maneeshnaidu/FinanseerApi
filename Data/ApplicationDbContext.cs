using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finanseer_api.Models;
using Microsoft.EntityFrameworkCore;

namespace finanseer_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
            
        }
        
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}