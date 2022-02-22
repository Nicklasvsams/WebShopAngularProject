using System;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;
using WebShopLibrary.DataAccessLayer.Database.Entities.Users;

namespace WebShopLibrary.BusinessAccessLayer.DTOs.PurchaseDTO
{
    public class PurchaseResponse
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public PurchaseUserResponse User { get; set; }
        public PurchaseProductResponse Product { get; set; }
    }

    public class PurchaseProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }

    public class PurchaseUserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
    }
}
