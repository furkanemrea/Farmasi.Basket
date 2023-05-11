using Farmasi.Basket.API.Controllers.Base;
using Farmasi.Basket.Common.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Farmasi.Basket.API.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet("get-test-user-id")]
        public Guid GetTestUserId()
        {
            return Constants.UserId;
        }
    }
}
