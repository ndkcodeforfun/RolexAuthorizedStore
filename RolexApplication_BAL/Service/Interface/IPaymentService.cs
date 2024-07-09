using RolexApplication_BAL.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolexApplication_BAL.Service.Interface
{
    public interface IPaymentService
    {
        Task<PaymentResponse> CreatePayment(PaymentRequest paymentRequest);
    }
}
