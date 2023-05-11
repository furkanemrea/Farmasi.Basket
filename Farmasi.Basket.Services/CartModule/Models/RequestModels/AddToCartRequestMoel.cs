using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Core.Application.Concrete.ExceptionTypes;
using Farmasi.Basket.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.CartModule.Models.RequestModels
{
    public class AddToCartRequestModel: IBaseRequestModel
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }


        public void UserValidate()
        {
            if (this.UserId != Constants.UserId) throw new BusinessException("User could not found");
        }

    }
    public class ProductWillAddToCardModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }  
    }
}
