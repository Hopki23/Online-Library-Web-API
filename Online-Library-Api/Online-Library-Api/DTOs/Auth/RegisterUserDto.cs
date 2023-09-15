namespace Online_Library_Api.DTOs.Auth
{
    using System.ComponentModel.DataAnnotations;
    public class RegisterUserDto
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
