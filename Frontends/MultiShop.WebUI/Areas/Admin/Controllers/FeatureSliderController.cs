using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/FeatureSlider")]
public class FeatureSliderController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FeatureSliderController(IHttpClientFactory httpClientFactory)
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
        ViewBag.v0 = "Öne Çıkan Slider Görsel İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Öne Çıkan Görseller";
        ViewBag.v3 = "Slider Öne Çıkan Görsel Listesi";

        var client = _httpClientFactory.CreateClient();//Api çağrımı yapılır.
        var responseMessage = await client.GetAsync("https://localhost:7070/api/FeatureSliders");

        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();//Gelen veri string formatta okunur.
            var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);
            return View(values);
        }

        return View();
    }

    [HttpGet]
    [Route("CreateFeatureSlider")]
    public IActionResult CreateFeatureSlider()
    {
        ViewBag.v0 = "Öne Çıkan Slider Görsel İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Öne Çıkan Görseller";
        ViewBag.v3 = "Slider Öne Çıkan Görsel Listesi";
        return View();
    }

    /// <summary>
    ///     Parametre olarak createFeatureSliderDto gönderilen değer json formatına dönüştülür. Dönüştülen değer content olarak atandı. jsonData türünde atananan, Encoding.UTF8 ile hangi dil desteğinin sağlandığı ve application/json mediator olduğu StringContent'de belirtilir.
    /// </summary>
    [HttpPost]
    [Route("CreateFeatureSlider")]
    public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
    {
        createFeatureSliderDto.Status = false;
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createFeatureSliderDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("https://localhost:7070/api/FeatureSliders/", stringContent);
        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });//Başarılı sonuçlandığında FeatureSlider'a ait Index sayfasına yönlenir.
        }
        return View();
    }

    [Route("DeleteFeatureSlider/{id}")]
    public async Task<IActionResult> DeleteFeatureSlider(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync("https://localhost:7070/api/FeatureSliders?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        }
        return View();
    }

    [HttpGet]
    [Route("UpdateFeatureSlider/{id}")]
    public async Task<IActionResult> UpdateFeatureSlider(string id)
    {
        ViewBag.v0 = "Öne Çıkan Slider Görsel İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Öne Çıkan Görseller";
        ViewBag.v3 = "Slider Öne Çıkan Görsel Listesi";

        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7070/api/FeatureSliders/" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateFeatureSliderDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    [Route("UpdateFeatureSlider/{Id}")]
    public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateFeatureSliderDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PutAsync("https://localhost:7070/api/FeatureSliders/", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        }
        return View();
    }
}
