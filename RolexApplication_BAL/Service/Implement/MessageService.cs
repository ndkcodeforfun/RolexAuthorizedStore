using AutoMapper;
using Azure.Core;
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
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MessageDtoResponse?> GetChatBoxByCustomerId(int customerId)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIDAsync(customerId);
                if (customer != null)
                {
                    List<MessageDtoResponse> response = new List<MessageDtoResponse>();
                    var chatHistory = (await _unitOfWork.ChatRequestRepository.GetAsync(filter: c => c.CustomerId == customerId, orderBy: c => c.OrderByDescending(s => s.SendTime))).FirstOrDefault();
                    if (chatHistory != null)
                    {
                        var message = _mapper.Map<MessageDtoResponse>(chatHistory);
                        return message;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MessageListDtoResponse>> GetChatBoxList()
        {
            try
            {
                List<MessageListDtoResponse> responses = new List<MessageListDtoResponse>();
                var customers = (await _unitOfWork.CustomerRepository.GetAsync()).ToList();
                if (customers.Any())
                {
                    foreach (var customer in customers)
                    {
                        var message = (await _unitOfWork.ChatRequestRepository.GetAsync(filter: c => c.CustomerId == customer.CustomerId, orderBy: c => c.OrderByDescending(m => m.SendTime))).FirstOrDefault();

                        if (message != null)
                        {
                            var response = new MessageListDtoResponse
                            {
                                CustomerName = customer.Name,
                                response = _mapper.Map<MessageDtoResponse>(message)
                            };
                            responses.Add(response);
                        }
                    }
                }
                return responses;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<(List<MessageDtoResponse>? response, string? CustomerName)> GetChatHistoryByCustomerId(int customerId)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIDAsync(customerId);
                if(customer != null)
                {
                    List<MessageDtoResponse> response = new List<MessageDtoResponse>();
                    var chatHistory = await _unitOfWork.ChatRequestRepository.GetAsync(filter: c => c.CustomerId == customerId, orderBy: c => c.OrderBy(s => s.SendTime));
                    if (chatHistory.Any())
                    {
                        foreach (var chat in chatHistory)
                        {
                            var message = _mapper.Map<MessageDtoResponse>(chat);
                            response.Add(message);
                        }
                    }
                    return (response, customer.Name);
                }
                return (null, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SendMessage(MessageDtoRequest request)
        {
            try
            {
                var chatRequest = _mapper.Map<ChatRequest>(request);
                await _unitOfWork.ChatRequestRepository.InsertAsync(chatRequest);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
