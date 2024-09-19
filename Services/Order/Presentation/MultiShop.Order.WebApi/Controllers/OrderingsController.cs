﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;

namespace MultiShop.Order.WebApi.Controllers;

[Authorize]//Login olma zorunluluğu eklendi.
[Route("api/[controller]")]
[ApiController]
public class OrderingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Mediator de handle metotlarına erişim için Send kullanılır.
    ///     IRequest'i miras alan sınıf çağrılır.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> OrderingList()
    {
        var values = await _mediator.Send(new GetOrderingQuery());
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderingById(int id)
    {
        var values = await _mediator.Send(new GetOrderingByIdQuery(id));
        return Ok(values);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrdering(CreateOrderingCommand command)
    {
        await _mediator.Send(command);
        return Ok("Sipariş başarıyla eklendi.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrdering(UpdateOrderingCommand command)
    {
        await _mediator.Send(command);
        return Ok("Sipariş başarıyla güncellendi.");
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveOrdering(int id)
    {
        await _mediator.Send(new RemoveOrderingCommand(id));
        return Ok("Sipariş başarıyla silindi.");
    }

    [HttpGet("GetOrderingByUserId/{id}")]
    public async Task<IActionResult> GetOrderingByUserId(string id)
    {
        var values = await _mediator.Send(new GetOrderingByUserIdQuery(id));
        return Ok(values);
    }
}
