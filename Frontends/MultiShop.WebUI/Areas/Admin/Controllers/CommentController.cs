using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[AllowAnonymous]
[Area("Admin")]
[Route("Admin/Comment")]
public class CommentController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    public CommentController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    ///     Serialize: Metin'den Json'a ve Ekle - Sil - Güncelle işlemlerinde yapılır.
    ///     Deserialize: Json'dan Metin'e ve Listele - Id'ye göre veri getirme işlemlerinde yapılır.
    /// </summary>
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewBag.v0 = "Yorum İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Yorumlar";
        ViewBag.v3 = "Yorum Listesi";

        var client = _httpClientFactory.CreateClient();//Api çağrımı yapılır.
        var responseMessage = await client.GetAsync("http://localhost:7075/api/Comments");//Yapılacak olan işlemin türü belirtilir. Comment mikro servisinde bulunan Comments controller'ına Get isteğinde bulunulur.

        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();//Gelen veri string formatta okunur.
            var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
            return View(values);
        }

        return View();
    }

    [Route("DeleteComment/{id}")]
    public async Task<IActionResult> DeleteComment(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync("http://localhost:7075/api/Comments?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Comment", new { area = "Admin" });
        }
        return View();
    }

    [HttpGet]
    [Route("UpdateComment/{id}")]
    public async Task<IActionResult> UpdateComment(string id)
    {
        ViewBag.v0 = "Yorum İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Yorumlar";
        ViewBag.v3 = "Yorum Güncelleme Sayfası";
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:7075/api/Comments/" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateCommentDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    [Route("UpdateComment/{Id}")]
    public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto)
    {
        updateCommentDto.Status = true;
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateCommentDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PutAsync("http://localhost:7075/api/Comments/", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Comment", new { area = "Admin" });
        }
        return View();
    }
}