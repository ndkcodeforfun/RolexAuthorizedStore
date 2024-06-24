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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateOrder(List<OrderProductDto> cartItems)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync()) {
                try
                {
                    decimal totalPrice = 0;
                    int customerId = cartItems[0].CustomerId;
                    List<OrderDetailDtoRequest> orderProducts = new List<OrderDetailDtoRequest>();
                    foreach (var cartItem in cartItems)
                    {
                        var product = await _unitOfWork.ProductRepository.GetByIDAsync(cartItem.ProductId);
                        totalPrice += (product.Price * cartItem.Quantity);
                        var orderProduct = new OrderDetailDtoRequest
                        {
                            ProductId = product.ProductId,
                            PricePerUnit = product.Price,
                            Quantity = cartItem.Quantity
                        };
                        orderProducts.Add(orderProduct);
                    }

                    // create order
                    var order = new Order
                    {
                        CustomerId = customerId,
                        TotalPrice = totalPrice,
                        Status = 1
                    };
                    await _unitOfWork.OrderRepository.InsertAsync(order);
                    await _unitOfWork.SaveAsync();

                    // create order detail
                    foreach (var orderProduct in orderProducts) {
                        var orderDetail = new OrderDetail
                        {
                            OrderId = order.OrderId,
                            ProductId = orderProduct.ProductId,
                            PricePerUnit = orderProduct.PricePerUnit,
                            Quantity = orderProduct.Quantity
                        };
                        await _unitOfWork.OrderDetailRepository.InsertAsync(orderDetail);
                        await _unitOfWork.SaveAsync();
                    }

                    // minus quantity of product
                    foreach (var orderProduct in orderProducts)
                    {
                        var product = await _unitOfWork.ProductRepository.GetByIDAsync(orderProduct.ProductId);
                        if (product.Quantity < orderProduct.Quantity)
                        {
                            throw new Exception("Not enough product in stock");
                        }
                        product.Quantity = product.Quantity - orderProduct.Quantity;
                        await _unitOfWork.ProductRepository.UpdateAsync(product);
                        await _unitOfWork.SaveAsync();
                    }

                    // delete items in cart
                    foreach (var cartItem in cartItems)
                    {
                        var item = await _unitOfWork.CartItemRepository.GetByIDAsync(cartItem.ItemId);
                        await _unitOfWork.CartItemRepository.DeleteAsync(item);
                        await _unitOfWork.SaveAsync();
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<int> ValidateItemInCart(List<OrderProductDto> cartItems)
        {
            try
            {
                foreach (var cartItem in cartItems)
                {
                    var item = (await _unitOfWork.CartItemRepository.GetAsync(filter: c => c.ItemId == cartItem.ItemId, includeProperties: "Product")).FirstOrDefault();
                    if (item == null)
                    {
                        return -1;
                    } else
                    {
                        if(item.Product.Status != 1)
                        {
                            return -2;
                        } else
                        {
                            if (cartItem.Quantity > item.Product.Quantity)
                            {
                                return -3;
                            }
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
