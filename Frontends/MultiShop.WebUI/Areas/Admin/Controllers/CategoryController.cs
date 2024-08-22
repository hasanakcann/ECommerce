using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[AllowAnonymous]
[Route("Admin/Category")]
public class CategoryController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CategoryController(IHttpClientFactory httpClientFactory)
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
        ViewBag.v0 = "Kategori İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Kategoriler";
        ViewBag.v3 = "Kategori Listesi";

        var client = _httpClientFactory.CreateClient();//Api çağrımı yapılır.
        var responseMessage = await client.GetAsync("https://localhost:7070/api/Categories");//Yapılacak olan işlemin türü belirtilir. Catalog mikro servisinde bulunan Categories controller'ına Get isteğinde bulunulur.

        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();//Gelen veri string formatta okunur.
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            return View(values);
        }

        return View();
    }

    [HttpGet]
    [Route("CreateCategory")]
    public IActionResult CreateCategory()
    {
        ViewBag.v0 = "Kategori İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Kategoriler";
        ViewBag.v3 = "Yeni Kategori Girişi";
        return View();
    }

    /// <summary>
    ///     Parametre olarak createCategoryDto gönderilen değer json formatına dönüştülür. Dönüştülen değer content olarak atandı. jsonData türünde atananan, Encoding.UTF8 ile hangi dil desteğinin sağlandığı ve application/json mediator olduğu StringContent'de belirtilir.
    /// </summary>
    [HttpPost]
    [Route("CreateCategory")]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createCategoryDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("https://localhost:7070/api/Categories/", stringContent);
        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            return RedirectToAction("Index", "Category", new {area = "Admin"});//Başarılı sonuçlandığında Category'ye ait Index sayfasına yönlenir.
        }
        return View();
    }

    [Route("DeleteCategory/{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync("https://localhost:7070/api/Categories?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }
        return View();
    }

    [HttpGet]
    [Route("UpdateCategory/{id}")]
    public async Task<IActionResult> UpdateCategory(string id)
    {
        ViewBag.v0 = "Kategori İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Kategoriler";
        ViewBag.v3 = "Kategori Güncelleme Sayfası";
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7070/api/Categories/" + id);
        if(responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    [Route("UpdateCategory/{Id}")]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PutAsync("https://localhost:7070/api/Categories/",stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }
        return View();
    }
}
