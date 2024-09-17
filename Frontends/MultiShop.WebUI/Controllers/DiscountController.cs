using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.DiscountServices;

namespace MultiShop.WebUI.Controllers;

public class DiscountController : Controller
{
    private readonly IDiscountService _discountService;
    private readonly IBasketService _basketService;
    public DiscountController(IDiscountService discountService, IBasketService basketService)
    {
        _discountService = discountService;
        _basketService = basketService;
    }

    [HttpGet]
    public PartialViewResult ConfirmDiscountCoupon()
    {
        return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmDiscountCoupon(string code)
    {
        var discountRate = await _discountService.GetDiscountCouponRate(code);
        var basketValues = await _basketService.GetBasket();
        var totalPriceWithTax = basketValues.TotalPrice / 100 * 20 + basketValues.TotalPrice;//%20 kdv hesaplaması
        var totalNewPriceWithDiscount = totalPriceWithTax - (totalPriceWithTax / 100 * discountRate);//indirim oranının uygulanması

        return RedirectToAction("Index", "ShoppingCart", new
        {
            code = code,
            discountRate = discountRate,
            totalNewPriceWithDiscount = totalNewPriceWithDiscount
        });
    }
}
