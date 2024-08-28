using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeatureDefaultComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FeatureDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
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
    }
}
