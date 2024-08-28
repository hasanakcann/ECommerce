using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[AllowAnonymous]
[Route("Admin/Feature")]
public class FeatureController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FeatureController(IHttpClientFactory httpClientFactory)
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
        ViewBag.v0 = "Anasayfa Öne Çıkan İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Öne Çıkan Alanlar";
        ViewBag.v3 = "Öne Çıkan Listesi";

        var client = _httpClientFactory.CreateClient();//Api çağrımı yapılır.
        var responseMessage = await client.GetAsync("https://localhost:7070/api/Features");//Yapılacak olan işlemin türü belirtilir. Catalog mikro servisinde bulunan Features controller'ına Get isteğinde bulunulur.

        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();//Gelen veri string formatta okunur.
            var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
            return View(values);
        }

        return View();
    }

    [HttpGet]
    [Route("CreateFeature")]
    public IActionResult CreateFeature()
    {
        ViewBag.v0 = "Anasayfa Öne Çıkan İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Öne Çıkan Alanlar";
        ViewBag.v3 = "Öne Çıkan Listesi";
        return View();
    }

    /// <summary>
    ///     Parametre olarak createFeatureDto gönderilen değer json formatına dönüştülür. Dönüştülen değer content olarak atandı. jsonData türünde atananan, Encoding.UTF8 ile hangi dil desteğinin sağlandığı ve application/json mediator olduğu StringContent'de belirtilir.
    /// </summary>
    [HttpPost]
    [Route("CreateFeature")]
    public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createFeatureDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("https://localhost:7070/api/Features/", stringContent);
        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            return RedirectToAction("Index", "Feature", new { area = "Admin" });//Başarılı sonuçlandığında Feature'a ait Index sayfasına yönlenir.
        }
        return View();
    }

    [Route("DeleteFeature/{id}")]
    public async Task<IActionResult> DeleteFeature(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync("https://localhost:7070/api/Features?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }
        return View();
    }

    [HttpGet]
    [Route("UpdateFeature/{id}")]
    public async Task<IActionResult> UpdateFeature(string id)
    {
        ViewBag.v0 = "Anasayfa Öne Çıkan İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Öne Çıkan Alanlar";
        ViewBag.v3 = "Öne Çıkan Listesi";

        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7070/api/Features/" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateFeatureDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    [Route("UpdateFeature/{Id}")]
    public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateFeatureDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PutAsync("https://localhost:7070/api/Features/", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }
        return View();
    }
}
