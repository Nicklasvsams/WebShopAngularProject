using System.ComponentModel.DataAnnotations;

namespace WebShopLibrary.BusinessAccessLayer.DTOs.UserDTO
{
    public class UserRequest
    {
        [Required]
        [StringLength(20, ErrorMessage = "Username can not be more than 20 characters long")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password can not be smaller than 8 characters and longer than 100 characters long")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Email can not be more than 100 characters long")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Usertype can not be more than 20 characters long")]
        public string UserType { get; set; }
    }
}
