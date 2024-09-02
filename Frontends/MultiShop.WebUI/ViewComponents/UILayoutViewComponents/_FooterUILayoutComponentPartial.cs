using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _FooterUILayoutComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FooterUILayoutComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();//Api çağrımı yapılır.
            var responseMessage = await client.GetAsync("https://localhost:7070/api/Abouts");//Yapılacak olan işlemin türü belirtilir. Catalog mikro servisinde bulunan Abouts controller'ına Get isteğinde bulunulur.

            if (responseMessage.IsSuccessStatusCode)//200 OK
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();//Gelen veri string formatta okunur.
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
