namespace Online_Library_Api.Data.Entities.User
{
    using Microsoft.AspNetCore.Identity;
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
        public ICollection<UserLiked> LikedBooks { get; set; } = new HashSet<UserLiked>();
    }
}
