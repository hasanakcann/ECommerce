namespace MultiShop.IdentityServer.Tools;

public class JwtTokenDefaults
{
    /// <summary>
    ///     Token'ı kimin dinlediğini belirtir.
    /// </summary>
    public const string ValidAudience = "http://localhost";

    /// <summary>
    ///     Token'ı kimin yayınladığını belirtir.
    /// </summary>
    public const string ValidIssuer = "http://localhost";

    /// <summary>
    ///     Token'da geçerli olan key belirtilir.
    /// </summary>
    public const string Key = "MultiShop..0102030405Asp.NetCore8.0.8*/+-";

    /// <summary>
    ///     Token'ın geçerlilik süresi belirtilir. 1 saat olarak tanımlanır.
    /// </summary>
    public const int Expire = 60;
}
