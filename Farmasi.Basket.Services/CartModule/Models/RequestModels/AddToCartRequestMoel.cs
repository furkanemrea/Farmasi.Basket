using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.CartModule.Models.RequestModels
{
    public class AddToCartRequestMoel
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }

    }
    public class ProductWillAddToCardModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }  
    }
}
