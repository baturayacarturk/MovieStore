using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class MovieStoreDB : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }


        public MovieStoreDB(IConfiguration configuration, DbContextOptions context) : base(context)
        {
            Configuration = configuration;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Order>().HasOne(o => o.Customer)
                    .WithMany()
                    .HasForeignKey(o => o.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>().HasOne(o => o.Movie)
                    .WithMany()
                    .HasForeignKey(o => o.MovieId)
                    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Movie>()
        .Property(e => e.Type)
        .HasConversion(
            v => v.ToString(),
            v => (MovieType)Enum.Parse(typeof(MovieType), v)
        );

        }

    }
}
