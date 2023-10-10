namespace Online_Library_Api.Data.Entities
{
    using Online_Library_Api.Data.Entities.User;
    public class UserLiked
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
