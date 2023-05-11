using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Core.Application.Concrete.ExceptionTypes;
using Farmasi.Basket.Core.Models.Base;

namespace Farmasi.Basket.Services.CartModule.Models.RequestModels
{
    public class GetCartByUserRequestModel: IBaseRequestModel
    {
        public Guid UserId { get; set; }

        public void Validate()
        {
            if (this.UserId != Constants.UserId) throw new BusinessException("User could not found");
        }
    }


}
