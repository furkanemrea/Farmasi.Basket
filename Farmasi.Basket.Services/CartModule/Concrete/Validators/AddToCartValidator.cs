using Farmasi.Basket.Core.Abstraction;
using Farmasi.Basket.Core.Application.Concrete.ExceptionTypes;
using Farmasi.Basket.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.CartModule.Concrete.Validators
{
    public  class AddToCartValidator: BaseValidator
    {
        public void Validate(Product product)
        {
            if (product == null) throw new ValidationException("Product could not found.");
        }
    }
}
