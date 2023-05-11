using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.ProductModule.Models.Response
{
    public class GetProductsResponseModel
    {
        public IEnumerable<ProductDetailModel> Products { get; set; }
    }
    public class ProductDetailModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CategoryNamee { get; set; }
        public decimal Price { get; set; }
    }
}
