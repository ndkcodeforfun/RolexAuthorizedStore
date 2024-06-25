using RolexApplication_Backend.Controllers;
using RolexApplication_BAL.ModelView;
using RolexApplication_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolexApplication_BAL.Service.Interface
{
    public interface IOrderService
    {
        Task<int> ValidateItemInCart(List<OrderProductDto> cartItems);
        Task CreateOrder(List<OrderProductDto> cartItems);
        Task<List<OrderDtoResponse>> GetAllOrders();
        Task<List<OrderDtoResponse>> GetOrdersByCustomerId(int CustomerId);
        Task<OrderDtoResponse?> GetOrderById(int id);
        Task<Order?> _getOrderById(int id);
        Task UpdateOrderStatus(Order order);
    }
}
