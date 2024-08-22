using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[AllowAnonymous]
[Route("Admin/Product")]
public class ProductController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductController(IHttpClientFactory httpClientFactory)
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
        ViewBag.v0 = "Ürün İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Ürünler";
        ViewBag.v3 = "Ürün Listesi";

        var client = _httpClientFactory.CreateClient();//Api çağrımı yapılır.
        var responseMessage = await client.GetAsync("https://localhost:7070/api/Products");//Yapılacak olan işlemin türü belirtilir. Catalog mikro servisinde bulunan Products controller'ına Get isteğinde bulunulur.

        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();//Gelen veri string formatta okunur.
            var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
            return View(values);
        }

        return View();
    }

    /// <summary>
    ///     Serialize: Metin'den Json'a ve Ekle - Sil - Güncelle işlemlerinde yapılır.
    ///     Deserialize: Json'dan Metin'e ve Listele - Id'ye göre veri getirme işlemlerinde yapılır.
    /// </summary>
    [HttpGet]
    [Route("CreateProduct")]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.v0 = "Ürün İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Ürünler";
        ViewBag.v3 = "Yeni Ürün Girişi";

        var client = _httpClientFactory.CreateClient();//Api çağrımı yapılır.
        var responseMessage = await client.GetAsync("https://localhost:7070/api/Categories");//Yapılacak olan işlemin türü belirtilir. Catalog mikro servisinde bulunan Categories controller'ına Get isteğinde bulunulur. Buradaki amacımız kategorilerin listelenmesidir.
        var jsonData = await responseMessage.Content.ReadAsStringAsync();//Gelen veri string formatta okunur.
        var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);//ResultCategoryDto Deserialize edilir.

        //İçerisinden öğe seçilebilir bir liste. Öğelerimiz kategorilerdir.
        List<SelectListItem> categoryValues = (from x in values
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,//Dropdown'da görüntülenecektir.
                                                   Value = x.CategoryId
                                               }).ToList();
        ViewBag.CategoryValues = categoryValues;//View'a taşınır.
        return View();
    }

    [HttpPost]
    [Route("CreateProduct")]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createProductDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("https://localhost:7070/api/Products/", stringContent);
        if (responseMessage.IsSuccessStatusCode)//200 OK
        {
            return RedirectToAction("Index", "Product", new { area = "Admin" });//Başarılı sonuçlandığında Product'a ait Index sayfasına yönlenir.
        }
        return View();
    }

    [Route("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync("https://localhost:7070/api/Products?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
        return View();
    }

    [HttpGet]
    [Route("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(string id)
    {
        ViewBag.v0 = "Ürün İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Ürünler";
        ViewBag.v3 = "Ürün Güncelleme Sayfası";

        var client_ = _httpClientFactory.CreateClient();//Api çağrımı yapılır.
        var responseMessage_ = await client_.GetAsync("https://localhost:7070/api/Categories");//Yapılacak olan işlemin türü belirtilir. Catalog mikro servisinde bulunan Categories controller'ına Get isteğinde bulunulur. Buradaki amacımız kategorilerin listelenmesidir.
        var jsonData_ = await responseMessage_.Content.ReadAsStringAsync();//Gelen veri string formatta okunur.
        var values_ = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData_);//ResultCategoryDto Deserialize edilir.

        //İçerisinden öğe seçilebilir bir liste. Öğelerimiz kategorilerdir.
        List<SelectListItem> categoryValues_ = (from x in values_
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,//Dropdown'da görüntülenecektir.
                                                   Value = x.CategoryId
                                               }).ToList();
        ViewBag.CategoryValues = categoryValues_;//View'a taşınır.

        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7070/api/Products/" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    [Route("UpdateProduct/{Id}")]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateProductDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PutAsync("https://localhost:7070/api/Products/", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
        return View();
    }
}
