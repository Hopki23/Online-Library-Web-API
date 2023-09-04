namespace Online_Library_Api.Data
{
    using Microsoft.EntityFrameworkCore;

    using Online_Library_Api.Data.Entities;
    public class WebApiContext : DbContext
    {
        public WebApiContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
