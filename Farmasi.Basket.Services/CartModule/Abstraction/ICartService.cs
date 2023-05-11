using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Services.CartModule.Models.RequestModels;
using Farmasi.Basket.Services.CartModule.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.CartModule.Abstraction
{
    public interface ICartService
    {
        Task<BaseResponse<AddToCartResponseModel>> AddToCart(AddToCartRequestMoel addToCartRequestMoel);
        Task<BaseResponse<UpdateItemCountOfProductResponsemodel>> UpdateItemCountOfProduct(UpdateItemCountOfProductRequestModel updateItemQuantityRequestModel);
        Task<BaseResponse<RemoveProductFromCartResponseModel>> RemoveProductFromCart(RemoveProductFromCartRequestModel requestModel);
        Task<BaseResponse<GetCartByUserResponseModel>> GetCartByUser(GetCartByUserRequestModel requestModel);


    }
}
