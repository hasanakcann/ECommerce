using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[AllowAnonymous]
[Route("Admin/SpecialOffer")]
public class SpecialOfferController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SpecialOfferController(IHttpClientFactory httpClientFactory)
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
        ViewBag.v0 = "Özel Teklif İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Özel Teklifler";
        ViewBag.v3 = "Özel Teklif ve Günün İndirim Listesi";

        var client = _httpClientFactory.CreateClient();//Api çağrımı yapılır.
        var responseMessage = await client.GetAsync("https://localhost:7070/api/SpecialOffers");//Yapılacak olan işlemin türü belirtilir. Catalog mikro servisinde bulunan SpecialOffers controller'ına Get isteğinde bulunulur.

        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();//Gelen veri string formatta okunur.
            var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(jsonData);
            return View(values);
        }

        return View();
    }

    [HttpGet]
    [Route("CreateSpecialOffer")]
    public IActionResult CreateSpecialOffer()
    {
        ViewBag.v0 = "Özel Teklif İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Özel Teklifler";
        ViewBag.v3 = "Özel Teklif ve Günün İndirim Listesi";

        return View();
    }

    /// <summary>
    ///     Parametre olarak createSpecialOfferDto gönderilen değer json formatına dönüştülür. Dönüştülen değer content olarak atandı. jsonData türünde atananan, Encoding.UTF8 ile hangi dil desteğinin sağlandığı ve application/json mediator olduğu StringContent'de belirtilir.
    /// </summary>
    [HttpPost]
    [Route("CreateSpecialOffer")]
    public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createSpecialOfferDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("https://localhost:7070/api/SpecialOffers/", stringContent);
        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });//Başarılı sonuçlandığında SpecialOffer'a ait Index sayfasına yönlenir.
        }
        return View();
    }

    [Route("DeleteSpecialOffer/{id}")]
    public async Task<IActionResult> DeleteSpecialOffer(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync("https://localhost:7070/api/SpecialOffers?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }
        return View();
    }

    [HttpGet]
    [Route("UpdateSpecialOffer/{id}")]
    public async Task<IActionResult> UpdateSpecialOffer(string id)
    {
        ViewBag.v0 = "Özel Teklif İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Özel Teklifler";
        ViewBag.v3 = "Özel Teklif ve Günün İndirim Listesi";

        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7070/api/SpecialOffers/" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateSpecialOfferDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    [Route("UpdateSpecialOffer/{Id}")]
    public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateSpecialOfferDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PutAsync("https://localhost:7070/api/SpecialOffers/", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }
        return View();
    }
}
