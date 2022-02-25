namespace WebShopLibrary.BusinessAccessLayer.DTOs.MonitorDTO
{
    public class MonitorResponse
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public int Size { get; set; }
        public int ReleaseYear { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public MonitorProductResponse Product { get; set; }
        public MonitorCategoryResponse Category { get; set; }
    }

    public class MonitorCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class MonitorProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }
}
