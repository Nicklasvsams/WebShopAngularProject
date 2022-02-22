using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopLibrary.DataAccessLayer.Database.Entities.Products.Games
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Publisher { get; set; }
        [Column(TypeName = "smallint")]
        public int PublishedYear { get; set; }
        [Column(TypeName = "nvarchar(32)")]
        public string Language { get; set; }
        [Column(TypeName = "nvarchar(32)")]
        public string Genre { get; set; }
        [Column(TypeName = "int")]
        public int CategoryId { get; set; }
        [Column(TypeName = "int")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
