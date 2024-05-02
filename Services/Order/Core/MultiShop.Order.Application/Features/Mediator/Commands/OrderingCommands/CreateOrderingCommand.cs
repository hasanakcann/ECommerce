using MediatR;

namespace MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands
{
    public class CreateOrderingCommand : IRequest
    {
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
    }
}
