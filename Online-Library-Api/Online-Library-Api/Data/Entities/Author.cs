namespace Online_Library_Api.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    public class Author
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int Age { get; set; }
        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
