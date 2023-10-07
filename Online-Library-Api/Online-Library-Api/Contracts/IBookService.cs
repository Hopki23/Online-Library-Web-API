namespace Online_Library_Api.Contracts
{
    using Online_Library_Api.Data.Entities;
    using Online_Library_Api.DTOs.Book;

    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<IEnumerable<Book>> GetMostLikedBooksAsync();
        Task<Book> CreateAsync(CreateBookDTO book);
        Task<BookDto> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<Book> UpdateAsync(UpdateBookDto book, Guid id);
    }
}
