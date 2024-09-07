using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MultiShop.WebUI.Controllers
{
    public class TestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        ///     Serialize: Metin'den Json'a ve Ekle - Sil - Güncelle işlemlerinde yapılır.
        ///     Deserialize: Json'dan Metin'e ve Listele - Id'ye göre veri getirme işlemlerinde yapılır.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            //Token değerinin UI'da gösterilmesi testi yapılmıştır. Bu yöntem güvenli bir yöntem değildir.
            string token;
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost:5001/connect/token"),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"client_id","MultiShopVisitorId" },
                        {"client_secret","multishopsecret" },
                        {"grant_type","client_credentials" }
                    })
                };

                using (var response = await httpClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var tokenResponse = JObject.Parse(content);
                        token = tokenResponse["access_token"].ToString();
                    }
                }
            }

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
    }
}
