using System.ComponentModel.DataAnnotations;

namespace WebShopLibrary.BusinessAccessLayer.DTOs.CategoryDTO
{
    public class CategoryRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name can not be longer than 50 characters long")]
        public string Name { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Language can not be longer than 2000 characters long")]
        public string Description { get; set; }
    }
}
