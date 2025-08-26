using LibraryServicesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryServicesAPI.DBContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books{ get; set; }
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.UserType).HasConversion<string>();
            modelBuilder.Entity<User>().Property(p => p.MaximumBorrowLimit).HasDefaultValue(5);
        }
    }
}
