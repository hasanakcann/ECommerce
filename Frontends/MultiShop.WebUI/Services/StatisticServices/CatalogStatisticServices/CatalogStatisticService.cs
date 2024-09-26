namespace MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;

public class CatalogStatisticService : ICatalogStatisticService
{
    private readonly HttpClient _httpClient;
    public CatalogStatisticService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<long> GetBrandCount()
    {
        var responseMessage = await _httpClient.GetAsync("statistics/getbrandcount");
        var values = await responseMessage.Content.ReadFromJsonAsync<long>();
        return values;
    }

    public async Task<long> GetCategoryCount()
    {
        var responseMessage = await _httpClient.GetAsync("statistics/getcategorycount");
        var values = await responseMessage.Content.ReadFromJsonAsync<long>();
        return values;
    }

    public async Task<string> GetMaxPriceProductName()
    {
        var responseMessage = await _httpClient.GetAsync("statistics/getmaxpriceproductname");
        var values = await responseMessage.Content.ReadAsStringAsync();
        return values;
    }

    public async Task<string> GetMinPriceProductName()
    {
        var responseMessage = await _httpClient.GetAsync("statistics/getminpriceproductname");
        var values = await responseMessage.Content.ReadAsStringAsync();
        return values;
    }

    public async Task<decimal> GetProductAveragePrice()
    {
        var responseMessage = await _httpClient.GetAsync("statistics/getproductaverageprice");
        var values = await responseMessage.Content.ReadFromJsonAsync<decimal>();
        return values;
    }

    public async Task<long> GetProductCount()
    {
        var responseMessage = await _httpClient.GetAsync("statistics/getproductcount");
        var values = await responseMessage.Content.ReadFromJsonAsync<long>();
        return values;
    }
}
