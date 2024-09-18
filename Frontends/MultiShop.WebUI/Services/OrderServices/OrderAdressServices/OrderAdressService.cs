using MultiShop.DtoLayer.OrderDtos.OrderAdressDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderAdressServices;

public class OrderAdressService : IOrderAdressService
{
    private readonly HttpClient _httpClient;
    public OrderAdressService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateOrderAddressAsync(CreateOrderAdressDto createOrderAdressDto)
    {
        await _httpClient.PostAsJsonAsync<CreateOrderAdressDto>("addresses", createOrderAdressDto);
    }
}
