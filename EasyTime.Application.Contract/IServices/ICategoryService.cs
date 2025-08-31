using EasyTime.Application.Contract.Dtos.Category;

namespace EasyTime.Application.Contract.IServices
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategories();
    }
}
