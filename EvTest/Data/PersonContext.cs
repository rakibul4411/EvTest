using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EvTest.Models
{
    public class PersonContext : DbContext
    {
        public PersonContext (DbContextOptions<PersonContext> options)
            : base(options)
        {
        }

        public DbSet<EvTest.Models.Person> Person { get; set; }
    }
}
