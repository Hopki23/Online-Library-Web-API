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

            modelBuilder.Entity<UserLiked>()
                .HasKey(ul => new { ul.ApplicationUserId, ul.BookId });

            modelBuilder.Entity<UserLiked>()
                .HasOne(ul => ul.ApplicationUser)
                .WithMany(u => u.LikedBooks)
                .HasForeignKey(ul => ul.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserLiked>()
                .HasOne(ul => ul.Book)
                .WithMany(b => b.LikedByUsers)
                .HasForeignKey(ul => ul.BookId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
