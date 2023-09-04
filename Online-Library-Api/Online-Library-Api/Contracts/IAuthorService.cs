namespace Online_Library_Api.Contracts
{
    using Online_Library_Api.Data.Entities;
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<Author> CreateAsync(Author author);
        Task<Author> UpdateAsync(Author author, Guid id);
    }
}
