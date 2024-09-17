﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

namespace MultiShop.WebUI.Controllers;

public class ShoppingCartController : Controller
{
    private readonly IProductService _productService;
    private readonly IBasketService _basketService;
    public ShoppingCartController(IProductService productService, IBasketService basketService)
    {
        _productService = productService;
        _basketService = basketService;
    }

    public async Task<IActionResult> Index(string code, int discountRate, decimal totalNewPriceWithDiscount)
    {
        ViewBag.code = code;
        ViewBag.discountRate = discountRate;
        ViewBag.totalNewPriceWithDiscount = totalNewPriceWithDiscount;
        ViewBag.directory1 = "Ana Sayfa";
        ViewBag.directory2 = "Ürünler";
        ViewBag.directory3 = "Sepetim";
        var values = await _basketService.GetBasket();
        ViewBag.total = values.TotalPrice; //Toplam ürün fiyatı
        var totalPriceWithTax = values.TotalPrice / 100 * 20 + values.TotalPrice;//%20 kdv hesaplaması
        ViewBag.totalPriceWithTax = totalPriceWithTax;
        var tax = values.TotalPrice / 100 * 20;
        ViewBag.tax = tax;
        return View();
    }

    public async Task<IActionResult> AddBasketItem(string id)
    {
        var values = await _productService.GetByIdProductAsync(id);
        var basketItems = new BasketItemDto
        {
            ProductId = values.ProductId,
            ProductName = values.ProductName,
            Price = values.ProductPrice,
            Quantity = 1,
            ProductImageUrl = values.ProductImageUrl
        };
        await _basketService.AddBasketItem(basketItems);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> RemoveBasketItem(string id)
    {
        await _basketService.RemoveBasketItem(id);
        return RedirectToAction("Index");
    }
}
