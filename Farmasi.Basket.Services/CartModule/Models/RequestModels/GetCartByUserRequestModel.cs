using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.CartModule.Models.RequestModels
{
    public class GetCartByUserRequestModel
    {
        public Guid UserId { get; set; }
    }
}
