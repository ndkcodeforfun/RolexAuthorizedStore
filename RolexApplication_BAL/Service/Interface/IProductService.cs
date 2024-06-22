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
        Task<bool> AddNewProduct(ProductVIew productVIew);

        Task<List<ProductVIew>> GetAllProducts(int CategoryId);

        Task<ProductDtoRequest> GetProductByID(int id);

        Task<bool> UpdateProduct(ProductVIew productVIew);

        Task<bool> StatusProduct(int id);
    }
}
