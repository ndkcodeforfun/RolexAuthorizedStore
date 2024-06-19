using RolexApplication_BAL.Service.Interface;
using RolexApplication_BAL.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RolexApplication_DAL.UnitOfWork.Interface;

namespace RolexApplication_BAL.Service.Implement
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<CategoryView>> GetAllCategories()
        {
            try
            {
                var categoryes = await _unitOfWork.CategoryRepository.FindAsync(c => c.Status == 1);
                if (categoryes.Count() == 0)
                {
                    return null;
                }
                List<CategoryView> categoryViews = new List<CategoryView>();
                foreach (var Type in categoryes)
                {
                    var categoryView = _mapper.Map<CategoryView>(Type);
                    categoryViews.Add(categoryView);
                }
                return categoryViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
