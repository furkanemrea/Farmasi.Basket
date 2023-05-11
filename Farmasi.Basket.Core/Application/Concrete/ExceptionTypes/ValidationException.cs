using Farmasi.Basket.Core.Application.Concrete.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Core.Application.Concrete.ExceptionTypes
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message)
        {
            this.AdditionalMessage = message;
        }
        // custom properties here.
    }
}
