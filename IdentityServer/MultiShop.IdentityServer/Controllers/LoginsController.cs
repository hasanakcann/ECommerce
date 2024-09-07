using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Tools;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginsController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    /// <summary>
    ///     PasswordSignInAsync metodu ile login parametreleri kontrol edilir. 
    ///     PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure) parametrelerini alır.
    ///     isPersistent -> Beni hatırla anlamındadır, false verildi. 
    ///     lockoutOnFailure -> Yeni bir kullanıcının kaydı gerçekleştikten sonra kullanıcı, UserName veya Password yanlış girdiği durumda AccessFailedCount değeri 1 artar. 5 olduğu durumda sistemde kullanıcı 5dk kilitlenir(lock). Bu durum gerçekleşmesin diye false set edildi.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> UserLogin(UserLoginDto userLoginDto)
    {
        var result = await _signInManager.PasswordSignInAsync(userLoginDto.UserName, userLoginDto.Password, false, false);
        var user = await _userManager.FindByNameAsync(userLoginDto.UserName);

        if (result.Succeeded)
        {
            GetCheckAppUserViewModel model = new GetCheckAppUserViewModel();
            model.UserName = userLoginDto.UserName;
            model.Id = user.Id;//Giriş yapan kullanıcının id bilgisidir.
            var token = JwtTokenGenerator.GenerateToken(model);
            return Ok(token);
        }
        else
        {
            return Ok("Kullanıcı Adı veya Şifre Hatalı.");
        }
    }
}
