﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.OfferDiscountDtos;
using MultiShop.Catalog.Services.OfferDiscountServices;

namespace MultiShop.Catalog.Controllers;

//[Authorize]//Login olma zorunluluğu eklendi.
[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class OfferDiscountsController : ControllerBase
{
    private readonly IOfferDiscountService _offerDiscountService;

    public OfferDiscountsController(IOfferDiscountService OfferDiscountService)
    {
        _offerDiscountService = OfferDiscountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOfferDiscountList()
    {
        var values = await _offerDiscountService.GetAllOfferDiscountAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOfferDiscountById(string id)
    {
        var values = await _offerDiscountService.GetByIdOfferDiscountAsync(id);
        return Ok(values);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto)
    {
        await _offerDiscountService.CreateOfferDiscountAsync(createOfferDiscountDto);
        return Ok("İndirim teklifi başarıyla eklendi.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOfferDiscount(string id)
    {
        await _offerDiscountService.DeleteOfferDiscountAsync(id);
        return Ok("İndirim teklifi başarıyla silindi.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto)
    {
        await _offerDiscountService.UpdateOfferDiscountAsync(updateOfferDiscountDto);
        return Ok("İndirim teklifi başarıyla güncellendi.");
    }
}
