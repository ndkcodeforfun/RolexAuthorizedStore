using RolexApplication_BAL.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolexApplication_BAL.Service.Interface
{
    public interface IProductService
    {
        Task<bool> AddNewProduct(ProductDtoRequest productVIew);

        Task<List<ProductVIew>> GetAllProducts(int CategoryId);

        Task<ProductVIew> GetProductByID(int id);

        Task<bool> UpdateProduct(ProductDtoRequest request, int id);

        Task<int> StatusProduct(int id);
    }
}
