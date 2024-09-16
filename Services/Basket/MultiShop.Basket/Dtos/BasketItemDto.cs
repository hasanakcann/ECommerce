﻿namespace MultiShop.Basket.Dtos
{
    /// <summary>
    ///     Sepet öğeleri tutulur.
    /// </summary>
    public class BasketItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
