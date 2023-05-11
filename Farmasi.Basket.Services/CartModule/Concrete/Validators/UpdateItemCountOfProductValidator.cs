using Farmasi.Basket.Core.Application.Concrete.ExceptionTypes;
using Farmasi.Basket.Data.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.CartModule.Concrete.Validators
{
    public class UpdateItemCountOfProductValidator
    {
        public void ThrowIfProductIsNull(Product product)
        {
            if (product == null)
            {
                throw new BusinessException("product could not found");
            }
        }

        public void ThrowIfUserHasNotCart(IDatabase _redisDb,string hashKey)
        {
            var existingCart = _redisDb.KeyExists(hashKey);

            if (!existingCart) throw new BusinessException("User has not a cart yet.");
             
        }
    }
}
