using System;

namespace MultiShop.IdentityServer.Tools;

public class TokenResponseViewModel
{
    //Constructor
    public TokenResponseViewModel(string token, DateTime expireDate)
    {
        Token = token;
        ExpireDate = expireDate;
    }

    public string Token { get; set; }
    public DateTime ExpireDate { get; set; }
}
