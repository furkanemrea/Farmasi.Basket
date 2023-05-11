using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmasi.Basket.Data.Models.Base;

namespace Farmasi.Basket.Data.Models
{
    public class Product:BaseEntity
    {
 
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
    }
}
