using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testNST.Models
{
    public class PSContext : DbContext
    {
        public PSContext(DbContextOptions<PSContext> options) : base(options)
        {
        }
        public DbSet<Person> Person { get; set; }
        public DbSet<Skill> Skill { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasMany(c => c.Skills);
            modelBuilder.Entity<Skill>().HasKey(c => new { c.PersonId, c.SkillName });
        }
    }
}
