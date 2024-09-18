namespace MultiShop.Order.Domain.Entities;

public class OrderDetail
{
    /// <summary>
    ///     Sipariş detay id bilgisidir.
    /// </summary>
    public int OrderDetailId { get; set; }
    /// <summary>
    ///     Ürün id bilgisidir. 
    /// </summary>
    public string ProductId { get; set; }
    /// <summary>
    ///     Ürün adı bilgisidir.
    /// </summary>
    public string ProductName { get; set; }
    /// <summary>
    ///     Ürün fiyat bilgisidir.
    /// </summary>
    public decimal ProductPrice { get; set; }
    /// <summary>
    ///     Ürün miktarı, adet bilgisidir.
    /// </summary>
    public int ProductAmount { get; set; }
    /// <summary>
    ///     Ürün toplam fiyat bilgisidir.
    /// </summary>
    public decimal ProductTotalPrice { get; set; }
    /// <summary>
    ///     Sipariş id bilgisidir.
    /// </summary>
    public int OrderingId { get; set; }
    /// <summary>
    ///     Sipariş modelidir.
    /// </summary>
    public Ordering Ordering { get; set; }
}
