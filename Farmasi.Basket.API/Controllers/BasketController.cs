using Farmasi.Basket.API.Controllers.Base;
using Farmasi.Basket.Core.Application.Concrete.ExceptionTypes;
using Microsoft.AspNetCore.Mvc;

namespace Farmasi.Basket.API.Controllers
{

    public class BasketController : BaseController
    {
        public BasketController()
        {

        }
        [HttpGet("exception-test")]
        public async Task<IActionResult> ExceptionTest()
        {
            throw new ValidationException("Hata Var");
        }

    }
}