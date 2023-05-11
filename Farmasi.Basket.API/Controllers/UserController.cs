using Farmasi.Basket.API.Controllers.Base;
using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Services.Publisher.Abstraction;
using Farmasi.Basket.Services.Publisher.Concrete.Models;
using Microsoft.AspNetCore.Mvc;

namespace Farmasi.Basket.API.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet("get-test-user-id")]
        public async Task<Guid> GetTestUserId()
        {

            return Constants.UserId;
        }
    }
}
