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
    public class CartItemService : ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddToCart(CartItemDtoRequest request)
        {
            try
            {
                var cartItem = _mapper.Map<CartItem>(request);
                await _unitOfWork.CartItemRepository.InsertAsync(cartItem);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
