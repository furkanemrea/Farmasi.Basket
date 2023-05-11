using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Core.Application.Concrete.ExceptionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.OrderModule.Concrete.Models.RequestModels
{
    public class CreatePaymentRequestModel
    {
        public Guid UserId { get; set; }

        public void Validate()
        {
            if (this.UserId != Constants.UserId) throw new BusinessException("User could not found.");
        }
    }
}
