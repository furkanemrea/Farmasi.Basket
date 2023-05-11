using Farmasi.Basket.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Data.Models
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public List<Product> Products { get; set; }

    }
}
