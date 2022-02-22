namespace WebShopLibrary.BusinessAccessLayer.DTOs.GameDTO
{
    public class GameResponse
    {
        public int Id { get; set; }
        public string Publisher { get; set; }
        public int PublishedYear { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }

        public GameProductResponse Product { get; set; }
        public GameCategoryResponse Category { get; set; }
    }

    public class GameCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class GameProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }
}
