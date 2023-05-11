using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Services.ProductModule.Models.Response;
using Farmasi.Basket.Services.Service.Product.Models.Request;
using Farmasi.Basket.Services.Service.Product.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.ProductModule.Abstraction
{
    public interface IProductService
    {
        Task<BaseResponse<CreateProductResponseModel>> CreateProduct(CreateProductRequestModel productCreateProductRequestModel);
        Task<BaseResponse<GetProductsResponseModel>> GetProducts();
    }
}
