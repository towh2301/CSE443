using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models.Context
{
    public class ApplicationDbContext : DbContext
    {

        // This is the constructor for the ApplicationDbContext class
        // It takes an instance of DbContextOptions as a parameter
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // This is the table that will be created in the database
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Loan> Loan { get; set; }   
        public DbSet<Carousel> Carousel { get; set; }

    }
}
