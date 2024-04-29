namespace MultiShop.Order.Domain.Entities
{
    public class Ordering
    {
        /// <summary>
        ///     Sipariş id bilgisidir.
        /// </summary>
        public int OrderingId { get; set; }
        /// <summary>
        ///     Kullanıcı id bilgisidir.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        ///     Sipariş toplam fiyat bilgisidir.
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        ///     Sipariş tarihi bilgisidir.
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        ///     Sipariş detay listesidir.
        /// </summary>
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
