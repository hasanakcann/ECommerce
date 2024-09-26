﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers;

[Authorize]//Login olma zorunluluğu eklendi.
[Route("api/[controller]")]
[ApiController]
public class DiscountsController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountsController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpGet]
    public async Task<IActionResult> DiscountCouponList()
    {
        var values = await _discountService.GetAllDiscountCouponAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDiscountCouponById(int id)
    {
        var values = await _discountService.GetByIdDiscountCouponAsync(id);
        return Ok(values);
    }

    [HttpGet("GetCodeDetailByCodeAsync")]
    public async Task<IActionResult> GetCodeDetailByCodeAsync(string code)
    {
        var values = await _discountService.GetCodeDetailByCodeAsync(code);
        return Ok(values);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createCouponDto)
    {
        await _discountService.CreateDiscountCouponAsync(createCouponDto);
        return Ok("İndirim kuponu başarıyla oluşturuldu.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDiscountCoupon(int id)
    {
        await _discountService.DeleteDiscountCouponAsync(id);
        return Ok("İndirim kuponu başarıyla silindi.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDto updateCouponDto)
    {
        await _discountService.UpdateDiscountCouponAsync(updateCouponDto);
        return Ok("İndirim kuponu başarıyla güncellendi.");
    }

    [HttpGet("GetDiscountCouponRate")]
    public IActionResult GetDiscountCouponRate(string code)
    {
        var values = _discountService.GetDiscountCouponRate(code);
        return Ok(values);
    }

    [HttpGet("GetDiscountCouponCount")]
    public async Task<IActionResult> GetDiscountCouponCount()
    {
        var values = await _discountService.GetDiscountCouponCount();
        return Ok(values);
    }
}
