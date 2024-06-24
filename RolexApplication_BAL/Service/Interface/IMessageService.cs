﻿using RolexApplication_BAL.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolexApplication_BAL.Service.Interface
{
    public interface IMessageService
    {
        public Task SendMessage(MessageDtoRequest request);

        public Task<(List<MessageDtoResponse>? response, string? CustomerName)> GetChatHistoryByCustomerId(int customerId);

        public Task<List<MessageListDtoResponse>> GetChatBoxList();

        public Task<MessageDtoResponse?> GetChatBoxByCustomerId(int customerId);
    }
}
