using Farmasi.Basket.Core.Models.Base;
using Farmasi.Basket.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.CartModule.Models.ResponseModels
{
    public class GetCartByUserResponseModel: IBaseResponseModel
    {
        public List<ProductOfCartModel> Products { get; set; }
    }
}
