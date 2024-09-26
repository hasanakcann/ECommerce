namespace MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices;

public class CommentStatisticService : ICommentStatisticService
{
    private readonly HttpClient _httpClient;
    public CommentStatisticService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> GetActiveCommentCount()
    {
        var responseMessage = await _httpClient.GetAsync("comments/getactivecommentcount");
        var values = await responseMessage.Content.ReadFromJsonAsync<int>();
        return values;
    }

    public async Task<int> GetPassiveCommentCount()
    {
        var responseMessage = await _httpClient.GetAsync("comments/getpassivecommentcount");
        var values = await responseMessage.Content.ReadFromJsonAsync<int>();
        return values;
    }

    public async Task<int> GetTotalCommentCount()
    {
        var responseMessage = await _httpClient.GetAsync("comments/gettotalcommentcount");
        var values = await responseMessage.Content.ReadFromJsonAsync<int>();
        return values;
    }
}
