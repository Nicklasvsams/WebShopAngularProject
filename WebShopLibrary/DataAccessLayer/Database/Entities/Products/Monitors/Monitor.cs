using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopLibrary.DataAccessLayer.Database.Entities.Products.Monitors
{
    public class Monitor
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Brand { get; set; }
        [Column(TypeName = "smallint")]
        public int Size { get; set; }
        [Column(TypeName = "int")]
        public int ReleaseYear { get; set; }
        [Column(TypeName = "int")]
        public int ProductId { get; set; }
        [Column(TypeName = "int")]
        public int CategoryId { get; set; }

        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
