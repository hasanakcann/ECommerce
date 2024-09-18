using MultiShop.DtoLayer.OrderDtos.OrderAdressDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderAdressServices;

public interface IOrderAdressService
{
    //Task<List<ResultOrderAdressDto>> GetAllOrderAdressAsync();
    Task CreateOrderAddressAsync(CreateOrderAdressDto createOrderAdressDto);
    //Task UpdateOrderAdressAsync(UpdateOrderAdressDto updateOrderAdressDto);
    //Task DeleteOrderAdressAsync(string id);
    //Task<UpdateOrderAdressDto> GetByIdOrderAdressAsync(string id);
}
