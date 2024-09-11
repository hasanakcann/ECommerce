namespace MultiShop.WebUI.Services.Interfaces;

/// <summary>
///     ClientCredential visitor için kullanıcı adı ve şifre bilgisine ihtiyaç olmadan token alan interface'dir.
/// </summary>
public interface IClientCredentialTokenService
{
    Task<string> GetToken();
}
