using Farmasi.Basket.API.Controllers.Base;
using Farmasi.Basket.Services.OrderModule.Abstraction;
using Farmasi.Basket.Services.OrderModule.Concrete.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Farmasi.Basket.API.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IOrderService _orderService;

        public PaymentController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create/payment")]
        public async Task<IActionResult> CreatePayment(CreatePaymentRequestModel createPaymentRequestModel)
        {
            var responseModel = await _orderService.CreatePayment(createPaymentRequestModel);
            return Ok(responseModel);
        }
    }
}
