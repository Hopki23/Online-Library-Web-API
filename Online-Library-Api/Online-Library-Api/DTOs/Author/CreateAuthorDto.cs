namespace Online_Library_Api.DTOs.Author
{
    using System.ComponentModel.DataAnnotations;
    public class CreateAuthorDto
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public string? ImageUrl { get; set; }
    }
}
