namespace MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;

public interface ICatalogStatisticService
{
    Task<long> GetCategoryCount();
    Task<long> GetProductCount();
    Task<long> GetBrandCount();
    Task<decimal> GetProductAveragePrice();
    Task<string> GetMaxPriceProductName();
    Task<string> GetMinPriceProductName();
}
