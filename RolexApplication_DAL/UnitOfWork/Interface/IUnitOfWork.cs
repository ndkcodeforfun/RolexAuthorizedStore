
using RolexApplication_DAL.Models;
using RolexApplication_DAL.Repository.Implement.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolexApplication_DAL.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {

        IGenericRepository<Cart> CartRepository { get; }
        IGenericRepository<CartItem> CartItemRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }

        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<OrderDetail> OrderDetailRepository { get; }

        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<ProductImage> ProductImageRepository { get; }


        Task SaveAsync();
    }
}
