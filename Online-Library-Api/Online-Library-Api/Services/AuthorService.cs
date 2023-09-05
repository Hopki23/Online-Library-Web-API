namespace Online_Library_Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Online_Library_Api.Contracts;
    using Online_Library_Api.Data;
    using Online_Library_Api.Data.Entities;
    using Online_Library_Api.DTOs.Author;
    using Online_Library_Api.DTOs.Book;

    public class AuthorService : IAuthorService
    {
        private readonly WebApiContext context;

        public AuthorService(WebApiContext context)
        {
            this.context = context;
        }

        public async Task<Author> CreateAsync(CreateAuthorDto author)
        {
            var createdAuthor = new Author()
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                Age = author.Age,
                ImageUrl = author.ImageUrl,
                Books = new List<Book>()
            };

            await this.context.Authors.AddAsync(createdAuthor);
            await this.context.SaveChangesAsync();

            return createdAuthor;
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

        public async Task<AuthorWithBooksDto> GetByIdAsync(Guid id)
        {
            var author = await context.Authors
                .Include(x => x.Books)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (author == null)
            {
                return null;
            }

            var authorDTO = new AuthorWithBooksDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Age = author.Age,
                ImageUrl = author.ImageUrl,
                Books = author.Books.Select(book => new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    DateOfPublish = book.DateOfPublish,
                    Resume = book.Resume,
                    GenreName = book.GenreName,
                    ImageUrl = book.ImageUrl,
                    Likes = book.Likes
                }).ToList()
            };

            return authorDTO;
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
