using Farmasi.Basket.API.Controllers.Base;
using Farmasi.Basket.Data.Models;
using Farmasi.Basket.Services.CartModule.Abstraction;
using Farmasi.Basket.Services.CartModule.Models.RequestModels;
using Farmasi.Basket.Services.ProductModule.Models.Response;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Farmasi.Basket.API.Controllers
{
    public class CartController : BaseController
    {

        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpPost("add-product-to-card")]
        public async Task<IActionResult> AddProductToCart(AddToCartRequestModel addToCartRequestMoel)
        {
            var responseModel = await _cartService.AddToCart(addToCartRequestMoel);
            return Ok(responseModel);
        }

        [HttpPost("remove-product-from-cart")]
        public async Task<IActionResult> RemoveProductFromCart(RemoveProductFromCartRequestModel removeProductFromCartRequestModel)
        {
            var responseModel = await _cartService.RemoveProductFromCart(removeProductFromCartRequestModel);
            return Ok(responseModel);
        }

        [HttpPost("update-product-count-of-cart")]
        public async Task<IActionResult> UpdateProductCountOfCart(UpdateItemCountOfProductRequestModel requestModel)
        {
            var responseModel = await _cartService.UpdateItemCountOfProduct(requestModel);
            return Ok(responseModel);
        }

        [HttpPost("get-cart-by-user")]
        public async Task<IActionResult> GetCartByUser(GetCartByUserRequestModel requestModel)
        {
            
            var responseModel = await _cartService.GetCartByUser(requestModel);
            return Ok(responseModel);

        }
    }
}
