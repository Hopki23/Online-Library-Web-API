namespace Online_Library_Api.DTOs.Book
{
    using Online_Library_Api.DTOs.Author;

    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime DateOfPublish { get; set; }
        public string Resume { get; set; }
        public string GenreName { get; set; }
        public string ImageUrl { get; set; }
        public int Likes { get; set; }
        public BaseAuthorDto Author { get; set; }
        public string CreatorId { get; set; }
    }
}
