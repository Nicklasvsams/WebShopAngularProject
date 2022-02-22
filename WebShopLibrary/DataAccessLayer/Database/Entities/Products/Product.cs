using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopLibrary.DataAccessLayer.Database.Entities.Products
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "nvarchar(2000)")]
        public string Description { get; set; }
        [Column(TypeName = "int")]
        public int Stock { get; set; }
    }
}
