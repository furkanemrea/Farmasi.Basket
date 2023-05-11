using Farmasi.Basket.API.Controllers.Base;
using Farmasi.Basket.Services.ProductModule.Abstraction;
using Farmasi.Basket.Services.Service.Product.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Farmasi.Basket.API.Controllers
{
    public class ProductController : BaseController
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateProductRequestModel createProductRequestModel)
        {
            var createProductResponseModel = await _productService.CreateProduct(createProductRequestModel);
            return Ok(createProductResponseModel);
        }
        [HttpGet("get-products")]
        public async Task<IActionResult> GetProducts()
        {
            var getProductsResponseModel = await _productService.GetProducts();
            return Ok(getProductsResponseModel);
        }
    }
}
