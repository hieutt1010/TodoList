using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using TodoApi.Core.Models.User;
using TodoApi.Models;

namespace TodoApi.Context
{
    public class MongoDbContext : DbContext
    {
        public DbSet<User> Users { get; init; }
        public DbSet<GoogleUser> GoogleUsers { get; init; }
        public DbSet<TodoItem> TodoItems { get; init; }
        public MongoDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasDiscriminator<string>("UserType")
               .HasValue<User>("User")
               .HasValue<GoogleUser>("GoogleUser");


            base.OnModelCreating(modelBuilder);
        }
    }
}