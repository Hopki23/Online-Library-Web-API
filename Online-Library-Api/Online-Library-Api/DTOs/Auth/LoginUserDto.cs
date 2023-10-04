namespace Online_Library_Api.DTOs.Auth
{
    using System.ComponentModel.DataAnnotations;
    public class LoginUserDto
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}