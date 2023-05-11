using AutoMapper;
using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Core.Application.Concrete.ExceptionTypes;
using Farmasi.Basket.Data.Abstraction;
using Farmasi.Basket.Data.Models;
using Farmasi.Basket.Services.CartModule.Abstraction;
using Farmasi.Basket.Services.CartModule.Models.RequestModels;
using Farmasi.Basket.Services.CartModule.Models.ResponseModels;
using MongoDB.Bson.IO;
using StackExchange.Redis;

namespace Farmasi.Basket.Services.CartModule.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IDatabase _redisDb;

        public CartService(ICartRepository cartRepository, IMapper mapper, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis-13882.c92.us-east-1-3.ec2.cloud.redislabs.com:13882,password=rSqMP2xLTKlhxzfVt9Wj58icKNHbbiXU");

            _redisDb = redis.GetDatabase();
        }

        public async Task<BaseResponse<AddToCartResponseModel>> AddToCart(AddToCartRequestMoel addToCartRequestMoel)
        {
            var selectedProduct = await _productRepository.GetById(addToCartRequestMoel.ProductId);

            if (selectedProduct == null)
            {
                throw new BusinessException("product could not found");
            }

            //var userCard = await _cartRepository.GetCartByUserId(addToCartRequestMoel.UserId);

            AddItemToCart(addToCartRequestMoel.UserId.ToString(), selectedProduct);

            return BaseResponse<AddToCartResponseModel>.Builder().SetSuccessCode().Build();
        }

        private void AddItemToCart(string userId, Product product)
        {
            var hashKey = $"cart:{userId}";

            var existingCart = _redisDb.KeyExists(hashKey);

            if (existingCart)
            {
                var serializedObject = _redisDb.HashGet(hashKey, nameof(ProductOfCartModel));

                var productOfCardModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductOfCartModel>>(serializedObject);

                if(productOfCardModel != null)
                {
                    var releatedProduct = productOfCardModel.Where(x => x.ProductId == product.Id).FirstOrDefault();
                    if(releatedProduct != null)
                    {
                        releatedProduct.Count++;
                    }
                }

                var hashFields = new HashEntry[]
                {
                    new HashEntry(nameof(ProductOfCartModel), Newtonsoft.Json.JsonConvert.SerializeObject(productOfCardModel))
                };

                _redisDb.HashSet(hashKey, hashFields);
            }
            else
            {
                List<ProductOfCartModel> productsOfCart = new();

                var productOfCartModel = new ProductOfCartModel()
                {
                    CategoryName = product.CategoryName,
                    Count = 1,
                    Price = product.Price,
                    Name = product.Name,
                    ProductId = product.Id
                };

                productsOfCart.Add(productOfCartModel);

                var hashFields = new HashEntry[]
                {
                       new HashEntry(nameof(ProductOfCartModel), Newtonsoft.Json.JsonConvert.SerializeObject(productsOfCart))
                };

                _redisDb.HashSet(hashKey, hashFields);
            }
        }


        public async Task<BaseResponse<UpdateItemCountOfProductResponsemodel>> UpdateItemCountOfProduct(UpdateItemCountOfProductRequestModel updateItemQuantityRequestModel)
        {
            var selectedProduct = await _productRepository.GetById(updateItemQuantityRequestModel.ProductId);

            if (selectedProduct == null)
            {
                throw new BusinessException("product could not found");
            }

            var hashKey = $"cart:{updateItemQuantityRequestModel.UserId}";

            var existingCart = _redisDb.KeyExists(hashKey);

            if (existingCart)
            {
                var serializedObject = _redisDb.HashGet(hashKey, nameof(ProductOfCartModel));

                var productOfCardList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductOfCartModel>>(serializedObject);

                var releatedProduct = productOfCardList.Where(x => x.ProductId == selectedProduct.Id).FirstOrDefault();

                releatedProduct.Count = updateItemQuantityRequestModel.Count;

                var hashFields = new HashEntry[]
                {
                    new HashEntry(nameof(ProductOfCartModel), Newtonsoft.Json.JsonConvert.SerializeObject(productOfCardList))
                };

                _redisDb.HashSet(hashKey, hashFields);

                return BaseResponse<UpdateItemCountOfProductResponsemodel>.Builder().SetSuccessCode().Build();
            }
            else
            {
                throw new BusinessException("User has not a cart yet.");
            }
        }

        public async Task<BaseResponse<RemoveProductFromCartResponseModel>> RemoveProductFromCart (RemoveProductFromCartRequestModel requestModel)
        {
            var selectedProduct = await _productRepository.GetById(requestModel.ProductId);

            if (selectedProduct == null)
            {
                throw new BusinessException("product could not found");
            }

            var hashKey = $"cart:{requestModel.UserId}";

            var existingCart = _redisDb.KeyExists(hashKey);

            if (existingCart)
            {
                var serializedObject = _redisDb.HashGet(hashKey, nameof(ProductOfCartModel));

                var productOfCardList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductOfCartModel>>(serializedObject);

                var hashFields = new HashEntry[]
                {
                    new HashEntry(nameof(ProductOfCartModel), Newtonsoft.Json.JsonConvert.SerializeObject(productOfCardList))
                };

                _redisDb.HashSet(hashKey, hashFields);

                return BaseResponse<RemoveProductFromCartResponseModel>.Builder().SetSuccessCode().Build();
            }
            else
            {
                throw new BusinessException("User has not a cart yet.");
            }
        }

        public async Task<BaseResponse<GetCartByUserResponseModel>> GetCartByUser(GetCartByUserRequestModel requestModel)
        {

            // to do : user validation here ! .

            var hashKey = $"cart:{requestModel.UserId}";

            var existingCart = await _redisDb.KeyExistsAsync(hashKey);

            if (existingCart)
            {
                var serializedObject = await _redisDb.HashGetAsync(hashKey, nameof(ProductOfCartModel));

                var productOfCardList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductOfCartModel>>(serializedObject);

                return BaseResponse<GetCartByUserResponseModel>.Builder().SetSuccessCode().SetData(new GetCartByUserResponseModel()
                {
                     Products = productOfCardList
                }).Build();
            }

            return BaseResponse<GetCartByUserResponseModel>.Builder().SetSuccessCode().Build();
        }
    }
}



//var products = new List<ProductOfCartModel>();

//var product = new ProductOfCartModel()
//{
//    Count = 1,
//    Name = "Glass",
//    CategoryName = "Category",
//    Price = 55,
//    ProductId = addToCartRequestMoel.ProductId.ToString(),
//};

//products.Add(product);
//var newCart = new Data.Models.Cart()
//{
//    Products = products,
//    UserId = Guid.NewGuid().ToString(),
//};

//await _cartRepository.Add(newCart);





//if (userCard == null)
//{
//    await _cartRepository.Add(new Cart()
//    {
//        UserId = addToCartRequestMoel.UserId.ToString(),
//        CreatedAt = DateTime.Now,
//        Products = new List<ProductOfCartModel> {
//            new ProductOfCartModel
//            {
//                 ProductId = selectedProduct.Id,
//                 CategoryName = selectedProduct.CategoryName,
//                 Count = 1,
//                 Price = selectedProduct.Price,
//                 Name = selectedProduct.Name,
//            }
//        }
//    });
//}
//else
//{
//    if (userCard.Products != null)
//    {

//        var productAlreadyExist = userCard.Products.Where(x => x.ProductId == selectedProduct.Id).Any();

//        if (productAlreadyExist)
//        {
//            userCard.Products.Where(x => x.ProductId == selectedProduct.Id).FirstOrDefault().Count++;
//        }
//        else
//        {
//            userCard.Products.Add(new ProductOfCartModel()
//            {
//                CategoryName = selectedProduct.CategoryName,
//                Count = 1,
//                Price = selectedProduct.Price,
//                Name = selectedProduct.Name,
//                ProductId = selectedProduct.Id
//            });
//        }
//    }
//    else
//    {
//        userCard.Products = new List<ProductOfCartModel>();

//        userCard.Products.Add(new ProductOfCartModel()
//        {
//            CategoryName = selectedProduct.CategoryName,
//            Count = 1,
//            Price = selectedProduct.Price,
//            Name = selectedProduct.Name,
//            ProductId = selectedProduct.Id
//        });
//    }

//    await _cartRepository.Update(userCard);

//}