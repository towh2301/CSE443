using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Loan> Loan { get; set; }   
        public DbSet<Carousel> Carousel { get; set; }

    }
}
