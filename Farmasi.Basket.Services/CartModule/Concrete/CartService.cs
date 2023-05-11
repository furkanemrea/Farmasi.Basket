using AutoMapper;
using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Core.Application.Concrete.ExceptionTypes;
using Farmasi.Basket.Data.Abstraction;
using Farmasi.Basket.Data.Models;
using Farmasi.Basket.Services.CartModule.Abstraction;
using Farmasi.Basket.Services.CartModule.Concrete.Helpers;
using Farmasi.Basket.Services.CartModule.Concrete.Validators;
using Farmasi.Basket.Services.CartModule.Models.RequestModels;
using Farmasi.Basket.Services.CartModule.Models.ResponseModels;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.IO;
using StackExchange.Redis;
using System.Collections.Generic;

namespace Farmasi.Basket.Services.CartModule.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IDatabase _redisDb;

        private readonly CartServiceHelper _cartServiceHelper;

        public CartService(ICartRepository cartRepository, IConfiguration configuration, IMapper mapper, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;

            string connectionString = configuration["Redis:ConnectionString"].ToString();

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);

            _redisDb = redis.GetDatabase();


            _cartServiceHelper = new(_redisDb);
        }

        public async Task<BaseResponse<AddToCartResponseModel>> AddToCart(AddToCartRequestModel addToCartRequestMoel)
        {
            AddToCartValidator addToCartValidator = new AddToCartValidator();

            addToCartRequestMoel.UserValidate();

            var product = await _productRepository.GetById(addToCartRequestMoel.ProductId);

            addToCartValidator.Validate(product);

            string hashKey = Util.GetCartHashKey(addToCartRequestMoel.UserId.ToString());

            bool existingCart = _redisDb.KeyExists(hashKey);

            if (existingCart)
            {
                _cartServiceHelper.IncreaseExistProductCountOnCart(product, hashKey);
            }
            else
            {
                _cartServiceHelper.CreateNewProductItem(product, hashKey);
            }


            return BaseResponse<AddToCartResponseModel>.Builder().SetSuccessCode().Build();
        }

        public async Task<BaseResponse<UpdateItemCountOfProductResponsemodel>> UpdateItemCountOfProduct(UpdateItemCountOfProductRequestModel updateItemQuantityRequestModel)
        {
            UpdateItemCountOfProductValidator updateItemCountOfProductValidator = new UpdateItemCountOfProductValidator();

            Product selectedProduct = await _productRepository.GetById(updateItemQuantityRequestModel.ProductId);

            updateItemCountOfProductValidator.ThrowIfProductIsNull(selectedProduct);

            string hashKey = Util.GetCartHashKey(updateItemQuantityRequestModel.UserId.ToString());


            updateItemCountOfProductValidator.ThrowIfUserHasNotCart(_redisDb, hashKey);

            _cartServiceHelper.UpdateProductCountOfCart(selectedProduct, hashKey, updateItemQuantityRequestModel.Count);

            return BaseResponse<UpdateItemCountOfProductResponsemodel>.Builder().SetSuccessCode().Build();
        }

        public async Task<BaseResponse<RemoveProductFromCartResponseModel>> RemoveProductFromCart(RemoveProductFromCartRequestModel requestModel)
        {

            RemoveProductFromCartValidator removeProductFromCartValidator = new RemoveProductFromCartValidator();

            Product selectedProduct = await _productRepository.GetById(requestModel.ProductId);

            removeProductFromCartValidator.ThrowIfProductIsNull(selectedProduct);

            string hashKey = Util.GetCartHashKey(requestModel.UserId.ToString());

            removeProductFromCartValidator.ThrowIfUserHasNotCart(_redisDb, hashKey);

            RedisValue serializedObject = _redisDb.HashGet(hashKey, nameof(ProductOfCartModel));

            List<ProductOfCartModel>? productOfCardList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductOfCartModel>>(serializedObject);

            var currentProducts = productOfCardList.Where(x => x.ProductId != requestModel.ProductId.ToString()).ToList();

            if(currentProducts == null || currentProducts.Count < 1)
            {
                await ClearBasket(requestModel.UserId.ToString());
            }

            var hashFields = new HashEntry[]
            {
                    new HashEntry(nameof(ProductOfCartModel), Newtonsoft.Json.JsonConvert.SerializeObject(currentProducts))
            };

            _redisDb.HashSet(hashKey, hashFields);

            return BaseResponse<RemoveProductFromCartResponseModel>.Builder().SetSuccessCode().Build();
        }

        public async Task<BaseResponse<GetCartByUserResponseModel>> GetCartByUser(GetCartByUserRequestModel requestModel)
        {

            requestModel.Validate();

            string hashKey = Util.GetCartHashKey(requestModel.UserId.ToString());

            bool existingCart = await _redisDb.KeyExistsAsync(hashKey);

            if (existingCart)
            {
                RedisValue serializedObject = await _redisDb.HashGetAsync(hashKey, nameof(ProductOfCartModel));

                List<ProductOfCartModel>? productOfCardList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductOfCartModel>>(serializedObject);

                return BaseResponse<GetCartByUserResponseModel>.Builder().SetSuccessCode().SetData(new GetCartByUserResponseModel()
                {
                    Products = productOfCardList
                }).Build();
            }

            return BaseResponse<GetCartByUserResponseModel>.Builder().SetSuccessCode().Build();
        }

        public async Task<BaseResponse<bool>> ClearBasket(string userId)
        {
            string hashKey = Util.GetCartHashKey(userId);

            bool existingCart = await _redisDb.KeyExistsAsync(hashKey);

            if (existingCart)
            {
                await _redisDb.KeyDeleteAsync(hashKey);
            }

            return BaseResponse<bool>.Builder().SetSuccessCode().Build();
        }

        public async Task<BaseResponse<string>> GetProductOfCartFromRedis(string userId)
        {
            var serializedObject = await _redisDb.HashGetAsync(Util.GetCartHashKey(userId), nameof(ProductOfCartModel));

            return BaseResponse<string>.Builder().SetMessage(serializedObject).SetSuccessCode().Build();
        }
    }
}
