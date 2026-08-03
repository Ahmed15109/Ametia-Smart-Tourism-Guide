using Grad.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebApplication4.Models;

namespace Grad.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }
        public DbSet<User>Users { get; set; }
        public DbSet<City>Cities { get; set; }
        public DbSet<Bank>Banks { get; set; }
        public DbSet<EntertainmentPlace>EntertainmentPlaces { get; set; }
        public DbSet<Hotel>Hotels { get; set; }
        public DbSet<Embasse>Embasses { get; set; }
        public DbSet<Restaurant>Restaurants { get; set; }
        public DbSet<Tourismt_Place>Tourismt_Places { get; set; }
        public DbSet<TransportProvider>transportProviders { get; set; }
        public DbSet<Type_place> types { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            builder.Entity<City>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<Type_place>().HasIndex(x => x.Name).IsUnique();

        }


    }
}
