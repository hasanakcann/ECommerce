using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers;

[Authorize] //Login olma zorunluluğu eklenir.
[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly CommentContext _context;

    public CommentsController(CommentContext commentContext)
    {
        _context = commentContext;
    }

    [HttpGet]
    public IActionResult GetAllCommentList()
    {
        var values = _context.UserComments.ToList();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public IActionResult GetCommentById(int id)
    {
        var value = _context.UserComments.Find(id);
        return Ok(value);
    }

    [HttpPost]
    public IActionResult CreateComment(UserComment userComment)
    {
        _context.UserComments.Add(userComment);
        _context.SaveChanges();
        return Ok("Yorum başarıyla eklendi.");
    }

    [HttpPut]
    public IActionResult UpdateComment(UserComment userComment)
    {
        _context.UserComments.Update(userComment);
        _context.SaveChanges();
        return Ok("Yorum başarıyla güncellendi.");
    }

    [HttpDelete]
    public IActionResult DeleteComment(int id)
    {
        var value = _context.UserComments.Find(id);
        _context.UserComments.Remove(value);
        _context.SaveChanges();
        return Ok("Yorum başarıyla silindi.");
    }

    [HttpGet("CommentListByProductId/{id}")]
    public IActionResult CommentListByProductId(string id)
    {
        var value = _context.UserComments.Where(x => x.ProductId == id).ToList();
        return Ok(value);
    }

    [HttpGet("GetActiveCommentCount")]
    public IActionResult GetActiveCommentCount()
    {
        int value = _context.UserComments.Where(x => x.Status == true).Count();
        return Ok(value);
    }

    [HttpGet("GetPassiveCommentCount")]
    public IActionResult GetPassiveCommentCount()
    {
        int value = _context.UserComments.Where(x => x.Status == false).Count();
        return Ok(value);
    }

    [HttpGet("GetTotalCommentCount")]
    public IActionResult GetTotalCommentCount()
    {
        int value = _context.UserComments.Count();
        return Ok(value);
    }
}
