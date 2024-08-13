namespace MultiShop.IdentityServer.Dtos
{
    /// <summary>
    ///     Kullanıcı girişi yapılırken gerekli olan bilgiler tanımlanır.
    ///     DBeaver'da MultiShopIdentityDb'de bulunan AspNetUsers tablosuna bakılabilir.
    /// </summary>
    public class UserRegisterDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
