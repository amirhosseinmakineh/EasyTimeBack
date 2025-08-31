using EasyTime.Application.Contract.Dtos.Category;
using EasyTime.Application.Contract.IServices;
using EasyTime.Model.IRepository;
using EasyTime.Model.Models;
using System.Threading.Tasks;

namespace EasyTime.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<int, Category> categoryRepository;

        public CategoryService(IBaseRepository<int, Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var result = await categoryRepository.GetAllEntities();
            return result.Where(x => x.IsDelete == false).Select(x => new CategoryDto()
            {
                CategoryId = x.Id,
                Name = x.Name,
            }).ToList();
        }
    }
}
