using Farmasi.Basket.Core.Application.Concrete.ExceptionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.Service.Product.Models.Request
{
    public class CreateProductRequestModel
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new ValidationException("product name cannot be null or empty");
            if (string.IsNullOrWhiteSpace(CategoryName)) throw new ValidationException("product category name cannot be null or empty");
            if (Price <= 0) throw new ValidationException("product price cannot be less than zero");
        }
    }
}
