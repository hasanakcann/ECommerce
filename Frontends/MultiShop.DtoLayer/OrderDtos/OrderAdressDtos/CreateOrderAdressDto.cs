namespace MultiShop.DtoLayer.OrderDtos.OrderAdressDtos;

public class CreateOrderAdressDto
{
    /// <summary>
    ///     Kullanıcı id bilgisidir.
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    ///     Kullanıcı adı
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    ///     Kullanıcı soyadı
    /// </summary>
    public string Surname { get; set; }
    /// <summary>
    ///     Email bilgisidir.
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    ///     Telefon numarasıdır.
    /// </summary>
    public string Phone { get; set; }
    /// <summary>
    ///     Ülke bilgisidir.
    /// </summary>
    public string Country { get; set; }
    /// <summary>
    ///     İlçe bilgisidir.
    /// </summary>
    public string District { get; set; }
    /// <summary>
    ///      Şehir bilgisidir.
    /// </summary>
    public string City { get; set; }
    /// <summary>
    ///     Detay bilgisidir. Apt.no cad.no bina no gibi bilgilerdir.
    /// </summary>
    public string Detail1 { get; set; }
    /// <summary>
    ///     Adres detayı bilgisidir.
    /// </summary>
    public string Detail2 { get; set; }
    /// <summary>
    ///     Posta kodu bilgisidir.
    /// </summary>
    public string ZipCode { get; set; }
}
