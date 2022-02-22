using System;
using System.ComponentModel.DataAnnotations;

namespace WebShopLibrary.BusinessAccessLayer.DTOs.PurchaseDTO
{
    public class PurchaseRequest
    {
        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
