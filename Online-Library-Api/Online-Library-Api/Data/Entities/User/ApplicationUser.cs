namespace Online_Library_Api.Data.Entities.User
{
    using Microsoft.AspNetCore.Identity;
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
