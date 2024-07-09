using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RolexApplication_BAL.ModelView;
using RolexApplication_BAL.Service.Interface;

namespace RolexApplication_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("vnpay-return")]
        public async Task<IActionResult> CreatePayment([FromQuery] PaymentRequest parameters)
        {
            try
            {
                if (parameters.vnp_BankTranNo == null) {
                    return BadRequest("Cancelled transaction");
                }
                var result = await _paymentService.CreatePayment(parameters);
                return result != null ? Ok(result) : NotFound("Order does not created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
