namespace Online_Library_Api.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Online_Library_Api.Data.Entities;
    using Online_Library_Api.Data.Entities.User;

    public class WebApiContext : IdentityDbContext<ApplicationUser>
    {
        public WebApiContext(DbContextOptions<WebApiContext> options) 
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
