using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.CartModule.Models.RequestModels
{
    public class UpdateItemCountOfProductRequestModel
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Count { get; set; }
    }
}
