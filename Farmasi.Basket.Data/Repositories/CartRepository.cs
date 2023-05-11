using Farmasi.Basket.Data.Abstraction;
using Farmasi.Basket.Data.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Data.Repositories
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(IMongoContext context) : base(context)
        {

        }


        public async Task<Cart> GetCartByUserId(Guid userId)
        {
            var releatedCart = await this.DbSet.FindAsync(Builders<Cart>.Filter.Eq($"{nameof(Cart.UserId)}",userId.ToString()));

            return releatedCart.FirstOrDefault();
        }
    }
}
