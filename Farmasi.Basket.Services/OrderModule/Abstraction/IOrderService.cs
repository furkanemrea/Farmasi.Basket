using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Services.OrderModule.Concrete.Models.RequestModels;
using Farmasi.Basket.Services.OrderModule.Concrete.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.OrderModule.Abstraction
{
    public interface IOrderService
    {
        Task<BaseResponse<CreatePaymentResponseModel>> CreatePayment(CreatePaymentRequestModel requestModel);
    }
}
