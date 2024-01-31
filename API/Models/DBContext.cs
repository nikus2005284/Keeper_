using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ClassLibrary;

namespace API.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {


        }
        public virtual DbSet<Division> division { get; set; }
        public virtual DbSet<Employees> employees { get; set; }
        public virtual DbSet<GroupUsers> groupUsers { get; set; }
        public virtual DbSet<Individ> individ { get; set; }
        public virtual DbSet<Target> target { get; set; }
        public virtual DbSet<User> user { get; set; }

    }
}
