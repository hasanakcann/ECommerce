namespace MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands
{
    public class RemoveAddressCommand
    {
        /// <summary>
        ///     İd bilgisidir.
        /// </summary>
        public int Id { get; set; }

        public RemoveAddressCommand(int id)
        {
            Id = id;
        }
    }
}
