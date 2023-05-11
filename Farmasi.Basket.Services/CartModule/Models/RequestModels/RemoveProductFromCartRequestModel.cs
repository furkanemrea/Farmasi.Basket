using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.CartModule.Models.RequestModels
{
    public class RemoveProductFromCartRequestModel
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
