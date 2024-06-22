using System;
using System.Collections.Generic;

namespace RolexApplication_DAL.Models
{
    public partial class ChatRequest
    {
        public int MessageId { get; set; }
        public int CustomerId { get; set; }
        public string? Content { get; set; }
        public DateTime? SendTime { get; set; }
        public int Status { get; set; }
    }
}
