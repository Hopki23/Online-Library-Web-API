namespace Online_Library_Api.DTOs.Author
{
    public class BaseAuthorDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string ImageUrl { get; set; }

    }
}
