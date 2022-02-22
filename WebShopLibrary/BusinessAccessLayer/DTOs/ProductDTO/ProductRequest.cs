using System;
using System.ComponentModel.DataAnnotations;

namespace WebShopLibrary.BusinessAccessLayer.DTOs.ProductDTO
{
    public class ProductRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name can not be longer than 50 characters long")]
        public string Name { get; set; }

        [Required]
        [Range(0, 50000, ErrorMessage = "Price must be between 0 and 50.000")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Language can not be longer than 2000 characters long")]
        public string Description { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "Stock must be between 1 and 100.000")]
        public int Stock { get; set; }
    }
}
