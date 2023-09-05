namespace Online_Library_Api.DTOs.Author
{
    using Online_Library_Api.DTOs.Book;
    public class AuthorWithBooksDto : BaseAuthorDto
    {
        public List<BookDto> Books { get; set; }
    }
}
