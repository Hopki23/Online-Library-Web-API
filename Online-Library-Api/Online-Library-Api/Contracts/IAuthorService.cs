namespace Online_Library_Api.Contracts
{
    using Online_Library_Api.Data.Entities;
    using Online_Library_Api.DTOs.Author;

    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<AuthorWithBooksDto> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<Author> CreateAsync(CreateAuthorDto author);
        Task<Author> UpdateAsync(Author author, Guid id);
    }
}
