﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Services.SpecialOfferServices;

namespace MultiShop.Catalog.Controllers;

//[Authorize]//Login olma zorunluluğu eklendi.
[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class SpecialOffersController : ControllerBase
{
    private readonly ISpecialOfferService _specialOfferService;

    public SpecialOffersController(ISpecialOfferService SpecialOfferService)
    {
        _specialOfferService = SpecialOfferService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSpecialOfferList()
    {
        var values = await _specialOfferService.GetAllSpecialOfferAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSpecialOfferById(string id)
    {
        var values = await _specialOfferService.GetByIdSpecialOfferAsync(id);
        return Ok(values);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
    {
        await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
        return Ok("Özel teklif başarıyla eklendi.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSpecialOffer(string id)
    {
        await _specialOfferService.DeleteSpecialOfferAsync(id);
        return Ok("Özel teklif başarıyla silindi.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
    {
        await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
        return Ok("Özel teklif başarıyla güncellendi.");
    }
}
