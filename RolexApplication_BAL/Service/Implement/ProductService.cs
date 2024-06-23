using AutoMapper;
using RolexApplication_BAL.ModelView;
using RolexApplication_BAL.Service.Interface;
using RolexApplication_DAL.Models;
using RolexApplication_DAL.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolexApplication_BAL.Service.Implement
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> AddNewProduct(ProductDtoRequest productVIew)
        {
            try
            {
                bool status = false;
                var checkCategory = _unitOfWork.CategoryRepository.GetByIDAsync(productVIew.CategoryId);
                if (checkCategory != null)
                {
                    var product = _mapper.Map<Product>(productVIew);
                    product.Status = 1;
                    await _unitOfWork.ProductRepository.InsertAsync(product);
                    await _unitOfWork.SaveAsync();
                    status = true;
                    return status;
                }
                else
                {
                    return status;
                }
                
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ProductVIew>> GetAllProducts(int CategoryId)
        {
            try
            {
                var products = new List<Product>();
                if(CategoryId == 0)
                {
                    products = (await _unitOfWork.ProductRepository.GetAsync(p => p.Status == 1)).ToList();
                } else
                {
                    products = (await _unitOfWork.ProductRepository.GetAsync(p => p.Status == 1 && p.CategoryId == CategoryId)).ToList();
                }
                if (products.Any())
                {
                    List<ProductVIew> list = new List<ProductVIew>();
                    foreach (var product in products)
                    {
                        var productVIew = _mapper.Map<ProductVIew>(product);
                        list.Add(productVIew);
                    }
                    return list;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<ProductVIew> GetProductByID(int id)
        {
            try
            {
                var product = (await _unitOfWork.ProductRepository.GetAsync(filter: p => p.ProductId == id, includeProperties: "Category")).FirstOrDefault();
                if (product != null)
                {
                    var productView = _mapper.Map<ProductVIew>(product);
                    return productView;
                }
                else
                {
                    return null;
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<int> StatusProduct(int id)
        {
            try
            {

                var checkProduct = await _unitOfWork.ProductRepository.GetByIDAsync(id);
                if (checkProduct != null)
                {
                    if(checkProduct.Status == 1)
                    {
                        checkProduct.Status = 0;
                        await _unitOfWork.ProductRepository.UpdateAsync(checkProduct);
                        await _unitOfWork.SaveAsync();
                        return 1;
                    }
                    else if (checkProduct.Status == 0)
                    {
                        if (checkProduct.Quantity == 0)
                        {
                            return 2;
                        }
                        checkProduct.Status = 1;
                        await _unitOfWork.ProductRepository.UpdateAsync(checkProduct);
                        await _unitOfWork.SaveAsync();
                        return 1;
                    } else
                    {
                        return 0;
                    }
                    
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<bool> UpdateProduct(ProductDtoRequest request, int id)
        {
            try
            {
                bool status = false;
                var checkCategory = _unitOfWork.CategoryRepository.GetByIDAsync(request.CategoryId);
                if (checkCategory != null)
                {
                    var checkProduct = await _unitOfWork.ProductRepository.GetByIDAsync(id);
                    if (checkProduct != null)
                    {
                        _mapper.Map(request, checkProduct);
                        await _unitOfWork.ProductRepository.UpdateAsync(checkProduct);
                        await _unitOfWork.SaveAsync();
                        status = true;
                        return status;
                    }
                    else
                    {
                        return status;
                    }
                }
                else
                {
                    return status;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
