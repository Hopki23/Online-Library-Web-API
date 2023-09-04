namespace Online_Library_Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Online_Library_Api.Contracts;
    using Online_Library_Api.Data;
    using Online_Library_Api.Data.Entities;

    public class AuthorService : IAuthorService
    {
        private readonly WebApiContext context;

        public AuthorService(WebApiContext context)
        {
            this.context = context;
        }

        public async Task<Author> CreateAsync(Author author)
        {
            await this.context.Authors.AddAsync(author);
            await this.context.SaveChangesAsync();

            return author;
        }

        public async Task DeleteAsync(Guid id)
        {
            var author = await this.context.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if (author == null)
            {
                throw new ArgumentException();
            }

            this.context.Authors.Remove(author);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync() => await this.context.Authors.ToListAsync();

        public async Task<Author> GetByIdAsync(Guid id)
        {
            return await this.context.Authors
                .Include(x => x.Books)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Author> UpdateAsync(Author author, Guid id)
        {
            var result = await this.context.Authors
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                throw new ArgumentException();
            }

            result.FirstName = author.FirstName;
            result.LastName = author.LastName;
            result.Age = author.Age;
            result.ImageUrl = author.ImageUrl;

            await this.context.SaveChangesAsync();

            return result;
        }
    }
}
