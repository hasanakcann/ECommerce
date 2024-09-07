using System.Security.Claims;

namespace MultiShop.WebUI.Services;

public class LoginService : ILoginService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public LoginService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    //NameIdentifier üzerinden kullanıcının bilgilerine erişim sağlanır.
    public string GetUserId => _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
}
