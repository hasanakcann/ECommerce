﻿namespace MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;

public class DiscountStatisticService : IDiscountStatisticService
{
    private readonly HttpClient _httpClient;
    public DiscountStatisticService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> GetDiscountCouponCount()
    {
        var responseMessage = await _httpClient.GetAsync("discounts/getdiscountcouponcount");
        var values = await responseMessage.Content.ReadFromJsonAsync<int>();
        return values;
    }
}
