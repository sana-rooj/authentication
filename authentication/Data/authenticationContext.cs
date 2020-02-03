using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace authentication.Models
{
    public class authenticationContext : DbContext
    {
        public authenticationContext (DbContextOptions<authenticationContext> options)
            : base(options)
        {
        }

        public DbSet<authentication.Models.UserCredentails> UserCredentails { get; set; }
    }
}
