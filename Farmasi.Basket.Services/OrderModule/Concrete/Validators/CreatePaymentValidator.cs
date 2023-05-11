using Farmasi.Basket.Core.Application.Concrete.ExceptionTypes;
using Farmasi.Basket.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.OrderModule.Concrete.Validators
{
    public class CreatePaymentValidator
    {
        public void ThrowIfProductsIsNull(string serializedObject)
        {
            if (string.IsNullOrEmpty(serializedObject))
                throw new BusinessException("Could not found products");
        }

        public void ThrowIfProductsCountLessThanOne(List<ProductOfCartModel> productOfCartModels)
        {
            if (productOfCartModels == null || productOfCartModels.Count < 1)
                throw new BusinessException("Could not found products");
        }
    }
}
