namespace MultiShop.Catalog.Dtos.CategoryDtos;

/// <summary>
///     Proje geliştikçe ihtiyaçlarda değişir bundan dolayı farklı DTO'lara ihtiyaç duyulur. DTO(Data Transfer Object) Entity yerine kullanılır. Veri alış verişi için Entity'leri kullanırsak güvenlik zafiyeti olur password gibi kolonlar taşınmak istenmeyebilir veya bazı kolonların gizlenmesi gerekebilir. Gizlenmesi gerektiği durumda DTO'larda gizlenmek istenen kolonlar yer almaz. DTO'larda farklı tablolardan gelen JOIN'lenmiş verilerde olabilir. JOIN için farklı nesnelerdeki(Entity)'lerin farklı kolonları DTO'ya yansıtılabilir. DTO'ya Complex Type'de denmektedir. Entity'den DTO'ya - DTO'dan ise Entity'ye dönüşüm için elle eşlemek gerekir. Elle eşlemek zor ve zahmetli olduğu için .NET'de AutoMapper kütüphanesi kullanılır.
/// </summary>
public class CreateCategoryDto
{
    public string CategoryName { get; set; }
    public string ImageUrl { get; set; }
}
