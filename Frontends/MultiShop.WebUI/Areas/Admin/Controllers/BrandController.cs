using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[AllowAnonymous]
[Route("Admin/Brand")]
public class BrandController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BrandController(IHttpClientFactory httpClientFactory)
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
        ViewBag.v0 = "Marka İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Markalar";
        ViewBag.v3 = "Marka Listesi";

        var client = _httpClientFactory.CreateClient();//Api çağrımı yapılır.
        var responseMessage = await client.GetAsync("https://localhost:7070/api/Brands");//Yapılacak olan işlemin türü belirtilir. Catalog mikro servisinde bulunan Brands controller'ına Get isteğinde bulunulur.

        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();//Gelen veri string formatta okunur.
            var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);
            return View(values);
        }

        return View();
    }

    [HttpGet]
    [Route("CreateBrand")]
    public IActionResult CreateBrand()
    {
        ViewBag.v0 = "Marka İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Markalar";
        ViewBag.v3 = "Yeni Marka Girişi";
        return View();
    }

    /// <summary>
    ///     Parametre olarak createBrandDto gönderilen değer json formatına dönüştülür. Dönüştülen değer content olarak atandı. jsonData türünde atananan, Encoding.UTF8 ile hangi dil desteğinin sağlandığı ve application/json mediator olduğu StringContent'de belirtilir.
    /// </summary>
    [HttpPost]
    [Route("CreateBrand")]
    public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createBrandDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("https://localhost:7070/api/Brands/", stringContent);
        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            return RedirectToAction("Index", "Brand", new { area = "Admin" });//Başarılı sonuçlandığında Brand'ye ait Index sayfasına yönlenir.
        }
        return View();
    }

    [Route("DeleteBrand/{id}")]
    public async Task<IActionResult> DeleteBrand(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync("https://localhost:7070/api/Brands?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Brand", new { area = "Admin" });
        }
        return View();
    }

    [HttpGet]
    [Route("UpdateBrand/{id}")]
    public async Task<IActionResult> UpdateBrand(string id)
    {
        ViewBag.v0 = "MArka İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Markalar";
        ViewBag.v3 = "Marka Güncelleme Sayfası";
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7070/api/Brands/" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateBrandDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    [Route("UpdateBrand/{Id}")]
    public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateBrandDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PutAsync("https://localhost:7070/api/Brands/", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Brand", new { area = "Admin" });
        }
        return View();
    }
}
