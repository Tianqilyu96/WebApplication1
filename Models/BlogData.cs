using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class BlogData : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public BlogData(DbContextOptions<BlogData> options) : base(options)
        {
            Database.EnsureCreated(); //make sure database created
        }
    }
}
