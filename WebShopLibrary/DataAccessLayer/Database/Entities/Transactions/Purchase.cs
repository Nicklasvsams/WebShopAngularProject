using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;
using WebShopLibrary.DataAccessLayer.Database.Entities.Users;

namespace WebShopLibrary.DataAccessLayer.Database.Entities.Transactions
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "Date")]
        public DateTime PurchaseDate { get; set; }
        [Column(TypeName = "int")]
        public int UserId { get; set; }
        [Column(TypeName = "int")]
        public int ProductId { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
