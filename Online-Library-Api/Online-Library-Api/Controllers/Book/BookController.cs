namespace Online_Library_Api.Controllers.Book
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Online_Library_Api.Contracts;
    using Online_Library_Api.DTOs.Book;


    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService service;

        public BookController(IBookService service)
        {
            this.service = service;
        }

        [HttpGet("most-liked")]
        public async Task<IActionResult> GetMostLikedBooks()
        {
            var books = await this.service.GetMostLikedBooksAsync();

            return Ok(books);
        }

        [HttpGet("all-books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await this.service.GetAllBooksAsync();

            return Ok(books);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await this.service.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody]CreateBookDTO book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdBook = await this.service.CreateAsync(book);

            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }

        [Authorize]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await this.service.DeleteAsync(id);
            }
            catch (Exception)
            {
                return NotFound("Book not found.");
            }

            return Ok();
        }

        [Authorize]
        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromBody] UpdateBookDto book, Guid id)
        {
            try
            {
                await this.service.UpdateAsync(book, id);

            }
            catch (Exception)
            {
                return NotFound("Book not found.");
            }

            return NoContent();
        }
    }
}
