using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Data.Abstraction;
using Farmasi.Basket.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IMongoContext context) : base(context)
        {

        }
    }
}
