using Farmasi.Basket.Data.Models.Base;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Data.Models
{
    public class Cart : BaseEntity
    {
        public string UserId { get; set; }
        public List<ProductOfCartModel> Products { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class ProductOfCartModel 
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
