using AutoMapper;
using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Data.Abstraction;
using Farmasi.Basket.Data.Models;
using Farmasi.Basket.Services.ProductModule.Abstraction;
using Farmasi.Basket.Services.ProductModule.Models.Response;
using Farmasi.Basket.Services.Service.Product.Models.Request;
using Farmasi.Basket.Services.Service.Product.Models.Response;

namespace Farmasi.Basket.Services.ProductModule.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<GetProductsResponseModel>> GetProducts()
        {
            GetProductsResponseModel response = new GetProductsResponseModel();

            var products = await _productRepository.GetAll();

            var mappedProducts = _mapper.Map<IEnumerable<ProductDetailModel>>(products);

            response.Products = mappedProducts;

            return BaseResponse<GetProductsResponseModel>.Builder().SetSuccessCode().SetData(response).Build();
        }

        public async Task<BaseResponse<CreateProductResponseModel>> CreateProduct(CreateProductRequestModel productCreateProductRequestModel)
        {
            productCreateProductRequestModel.Validate();

            var mappedProduct = _mapper.Map<Product>(productCreateProductRequestModel);

            await _productRepository.Add(mappedProduct);

            return BaseResponse<CreateProductResponseModel>.Builder().SetSuccessCode().Build();
        }
    }
}