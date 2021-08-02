using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication1.Models
{
    public class IdentityData : IdentityDbContext<IdentityUser>
    {
        public IdentityData(DbContextOptions<IdentityData> options) : base(options)
        {
            Database.EnsureCreated(); //make sure database created
        }
    }
}
