﻿namespace WebShopLibrary.BusinessAccessLayer.DTOs.ProductDTO
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }
}
