using RolexApplication_BAL.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolexApplication_BAL.Service.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryView>> GetAllCategories();
        Task<CategoryView> GetCategoryById(int id);
        Task CreateCategory(CategoryDtoRequest request);
        Task UpdateCategory(int CategoryId, CategoryDtoRequest request);
        Task DeleteCategory(int CategoryId);
    }
}
