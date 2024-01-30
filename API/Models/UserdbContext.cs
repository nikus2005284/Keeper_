using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ClassLibrary;

namespace API.Models
{
    public class UserdbContext : DbContext
    {
        public UserdbContext(DbContextOptions<UserdbContext> options)
            : base(options)
        {


        }
        public virtual DbSet<User> tests { get; set; }
    }
}
