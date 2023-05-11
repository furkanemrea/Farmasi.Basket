using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Core.Application.Concrete.ExceptionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.CartModule.Models.RequestModels
{
    public class GetCartByUserRequestModel
    {
        public Guid UserId { get; set; }

        public void Validate()
        {
            if (this.UserId != Constants.UserId) throw new BusinessException("User could not found");
        }
    }


}
