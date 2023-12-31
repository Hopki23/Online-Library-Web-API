﻿namespace Online_Library_Api.Services
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using Online_Library_Api.Contracts;
    using Online_Library_Api.Data;
    using Online_Library_Api.Data.Entities;
    using Online_Library_Api.DTOs.Author;
    using Online_Library_Api.DTOs.Book;

    public class BookService : IBookService
    {
        private readonly WebApiContext context;

        public BookService(WebApiContext context)
        {
            this.context = context;
        }

        public async Task<Book> CreateAsync(CreateBookDTO book)
        {
            var createdBook = new Book()
            {
                Title = book.Title,
                DateOfPublish = book.DateOfPublish,
                Resume = book.Resume,
                GenreName = book.GenreName,
                ImageUrl = book.ImageUrl,
                AuthorId = book.AuthorId,
                ApplicationUserId = book.UserId
            };

            await this.context.Books.AddAsync(createdBook);
            await this.context.SaveChangesAsync();

            return createdBook;
        }

        public async Task DeleteAsync(Guid id)
        {
            var book = await this.context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            this.context.Books.Remove(book);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync() => await this.context.Books.ToListAsync();

        public async Task<BookDto> GetByIdAsync(Guid id)
        {
            var book = await this.context.Books
               .Include(x => x.Author)
               .FirstOrDefaultAsync(x => x.Id == id);

            var creator = await this.context.Users
                .Where(u => u.Id == book.ApplicationUserId)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            var bookDto = new BookDto()
            {
                Id = book.Id,
                Title = book.Title,
                DateOfPublish = book.DateOfPublish,
                Resume = book.Resume,
                GenreName = book.GenreName,
                ImageUrl = book.ImageUrl,
                Likes = book.Likes,
                CreatorId = creator!.Id,
                Author = new BaseAuthorDto()
                {
                    Id = book.Author.Id,
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName,
                    Age = book.Author.Age,
                    ImageUrl = book.Author.ImageUrl
                }
            };

            return bookDto;
        }

        public async Task<IEnumerable<Book>> GetMostLikedBooksAsync()
        {
            return await this.context.Books
                .OrderBy(x => x.Likes)
                .Take(3)
                .ToListAsync();
        }

        public async Task LikeBookAsync(Guid bookId, string userId)
        {
            var user = await this.context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.LikedBooks)
                .FirstOrDefaultAsync();

            var book = await this.context.Books
                    .Where(x => x.Id == bookId)
                    .FirstOrDefaultAsync();

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            var userLikedBook = user.LikedBooks.FirstOrDefault(ub => ub.BookId == bookId);

            if (userLikedBook == null)
            {
                user.LikedBooks.Add(new UserLiked
                {
                    ApplicationUser = user,
                    Book = book
                });

                book.Likes += 1;

                await this.context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Book is already liked");
            }
        }

        public async Task<Book> UpdateAsync(UpdateBookDto book, Guid id)
        {
            var result = await this.context.Books
                 .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                throw new Exception("Book not found");
            }

            result.Title = book.Title;
            result.DateOfPublish = book.DateOfPublish;
            result.Resume = book.Resume;
            result.GenreName = book.GenreName;
            result.ImageUrl = book.ImageUrl;
            result.AuthorId = book.AuthorId;

            await this.context.SaveChangesAsync();

            return result;
        }
    }
}
