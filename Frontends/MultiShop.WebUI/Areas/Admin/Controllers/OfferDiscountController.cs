using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[AllowAnonymous]
[Route("Admin/OfferDiscount")]
public class OfferDiscountController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OfferDiscountController(IHttpClientFactory httpClientFactory)
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
        ViewBag.v0 = "İndirim Teklif İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "İndirim Teklifleri";
        ViewBag.v3 = "İndirim Teklif Listesi";

        var client = _httpClientFactory.CreateClient();//Api çağrımı yapılır.
        var responseMessage = await client.GetAsync("https://localhost:7070/api/OfferDiscounts");//Yapılacak olan işlemin türü belirtilir. Catalog mikro servisinde bulunan OfferDiscounts controller'ına Get isteğinde bulunulur.

        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();//Gelen veri string formatta okunur.
            var values = JsonConvert.DeserializeObject<List<ResultOfferDiscountDto>>(jsonData);
            return View(values);
        }

        return View();
    }

    [HttpGet]
    [Route("CreateOfferDiscount")]
    public IActionResult CreateOfferDiscount()
    {
        ViewBag.v0 = "İndirim Teklif İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "İndirim Teklifleri";
        ViewBag.v3 = "İndirim Teklif Listesi";
        return View();
    }

    /// <summary>
    ///     Parametre olarak createOfferDiscountDto gönderilen değer json formatına dönüştülür. Dönüştülen değer content olarak atandı. jsonData türünde atananan, Encoding.UTF8 ile hangi dil desteğinin sağlandığı ve application/json mediator olduğu StringContent'de belirtilir.
    /// </summary>
    [HttpPost]
    [Route("CreateOfferDiscount")]
    public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createOfferDiscountDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("https://localhost:7070/api/OfferDiscounts/", stringContent);
        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });//Başarılı sonuçlandığında OfferDiscount'ye ait Index sayfasına yönlenir.
        }
        return View();
    }

    [Route("DeleteOfferDiscount/{id}")]
    public async Task<IActionResult> DeleteOfferDiscount(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync("https://localhost:7070/api/OfferDiscounts?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
        }
        return View();
    }

    [HttpGet]
    [Route("UpdateOfferDiscount/{id}")]
    public async Task<IActionResult> UpdateOfferDiscount(string id)
    {
        ViewBag.v0 = "İndirim Teklif İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "İndirim Teklifleri";
        ViewBag.v3 = "İndirim Teklif Listesi";

        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7070/api/OfferDiscounts/" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateOfferDiscountDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    [Route("UpdateOfferDiscount/{Id}")]
    public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateOfferDiscountDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PutAsync("https://localhost:7070/api/OfferDiscounts/", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
        }
        return View();
    }
}
