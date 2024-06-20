﻿using AutoMapper;
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
        public async Task<bool> AddNewProduct(ProductVIew productVIew)
        {
            try
            {
                bool status = false;
                var checkCategory = _unitOfWork.CategoryRepository.GetByIDAsync(productVIew.CategoryId);
                if (checkCategory != null)
                {
                    productVIew.Status = 1;
                    var product = _mapper.Map<Product>(productVIew);
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

        public async Task<IEnumerable<ProductVIew>> GetAllProducts()
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.FindAsync(p => p.Status == 1);
                if (products != null)
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
                var product = await _unitOfWork.ProductRepository.GetByIDAsync(id);
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

        public async Task<bool> StatusProduct(int id)
        {
            try
            {

                var checkProduct = await _unitOfWork.ProductRepository.GetByIDAsync(id);
                if (checkProduct != null)
                {
                    if(checkProduct.Status == 1)
                    {
                        checkProduct.Status = 2;
                        await _unitOfWork.ProductRepository.UpdateAsync(checkProduct);
                        await _unitOfWork.SaveAsync();
                        return true;
                    }
                    else 
                    {
                        checkProduct.Status = 1;
                        await _unitOfWork.ProductRepository.UpdateAsync(checkProduct);
                        await _unitOfWork.SaveAsync();
                        return true;
                    }    
                    
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<bool> UpdateProduct(ProductVIew productVIew)
        {
            try
            {
                bool status = false;
                var checkCategory = _unitOfWork.CategoryRepository.GetByIDAsync(productVIew.CategoryId);
                if (checkCategory != null)
                {
                    var checkProduct = await _unitOfWork.ProductRepository.GetByIDAsync(productVIew.ProductId);
                    if (checkProduct != null)
                    {
                        _mapper.Map(productVIew, checkProduct);
                        checkProduct.Status = 1;
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