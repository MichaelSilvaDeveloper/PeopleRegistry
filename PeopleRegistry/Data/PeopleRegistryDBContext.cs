using Microsoft.EntityFrameworkCore;
using PeopleRegistry.Data.Map;
using PeopleRegistry.Models;

namespace PeopleRegistry.Data
{
    public class PeopleRegistryDBContext : DbContext
    {
        public PeopleRegistryDBContext(DbContextOptions<PeopleRegistryDBContext> options) : base(options)
        {
        }

        public DbSet<Person> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}