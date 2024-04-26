using MultiShop.Catalog.Dtos.CategoryDtos;

namespace MultiShop.Catalog.Services.CategoryServices
{
    /// <summary>
    ///     Category ile ilgili gerçekleşen crud işlemleri tutulur. 
    ///     Task ile asenkron bir yapı kullanıldı. 
    ///     GetAllCategoryAsync Tüm veriler getirilir.
    ///     CreateCategoryAsync Ekleme işlemi yapılır.
    ///     UpdateCategoryAsync Güncelleme işlemi yapılır.
    ///     DeleteCategoryAsync Silme işlemi yapılır.
    ///     GetByIdCategoryAsync Id ye göre veri getirir.
    /// </summary>
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteCategoryAsync(string id);
        Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id);
    }
}
