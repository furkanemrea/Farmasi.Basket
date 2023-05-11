using Farmasi.Basket.Data.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.CartModule.Concrete.Helpers
{
    public class CartServiceHelper
    {
        private IDatabase _redisDb;
        public CartServiceHelper(IDatabase redisDb)
        {
            _redisDb = redisDb;
        }

        public void IncreaseExistProductCountOnCart(Product product, string hashKey)
        {
            var serializedObject = _redisDb.HashGet(hashKey, nameof(ProductOfCartModel));

            var productOfCardModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductOfCartModel>>(serializedObject);

            if (productOfCardModel != null && productOfCardModel.Count > 0)
            {
                var releatedProduct = productOfCardModel.Where(x => x.ProductId == product.Id).FirstOrDefault();
                if (releatedProduct != null)
                {
                    releatedProduct.Count++;
                }
            }
            else
            {
                productOfCardModel.Add(new()
                {
                    CategoryName = product.CategoryName,
                    Name = product.Name,
                    Price = product.Price,
                    ProductId = product.Id,
                });
            }

            var hashFields = new HashEntry[]
            {
                    new HashEntry(nameof(ProductOfCartModel), Newtonsoft.Json.JsonConvert.SerializeObject(productOfCardModel))
            };

            _redisDb.HashSet(hashKey, hashFields);
        }

        public void CreateNewProductItem(Product product, string hashKey)
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

        public void UpdateProductCountOfCart(Product product, string hashKey, int count)
        {
            var serializedObject = _redisDb.HashGet(hashKey, nameof(ProductOfCartModel));

            var productOfCardList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductOfCartModel>>(serializedObject);

            var releatedProduct = productOfCardList.Where(x => x.ProductId == product.Id).FirstOrDefault();

            releatedProduct.Count = count;

            var hashFields = new HashEntry[]
            {
                    new HashEntry(nameof(ProductOfCartModel), Newtonsoft.Json.JsonConvert.SerializeObject(productOfCardList))
            };

            _redisDb.HashSet(hashKey, hashFields);

        }
    }
}
