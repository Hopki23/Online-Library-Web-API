namespace Online_Library_Api.Controllers.Book
{
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

        [HttpGet]
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
