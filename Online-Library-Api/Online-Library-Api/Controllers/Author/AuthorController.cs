namespace Online_Library_Api.Controllers.Author
{
    using Microsoft.AspNetCore.Mvc;

    using Online_Library_Api.Contracts;
    using Online_Library_Api.Data.Entities;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService service;

        public AuthorController(IAuthorService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await this.service.GetAllAuthorsAsync();

            return Ok(authors);
        }


        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var author = await this.service.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAuthor = await this.service.CreateAsync(author);

            return CreatedAtAction(nameof(GetById), new { id = createdAuthor.Id }, createdAuthor);
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
                return NotFound("Author not found.");
            }

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Update(Author author, Guid id)
        {
            await this.service.UpdateAsync(author, id);

            return Ok(author);
        }
    }
}
