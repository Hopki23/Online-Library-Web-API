namespace Online_Library_Api.DTOs.Book
{
    using System.ComponentModel.DataAnnotations;
    public class CreateBookDTO
    {
        [Required]
        public string Title { get; set; } = null!;
        public DateTime DateOfPublish { get; set; }

        [Required]
        public string Resume { get; set; } = null!;

        [Required]
        public string GenreName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int Likes { get; set; }
        public Guid AuthorId { get; set; }

        [Required]
        public string UserId { get; set; } = null!;
    }
}
