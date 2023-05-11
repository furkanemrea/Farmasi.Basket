using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Common.Concrete
{
    public sealed class Constants
    {
        public static Guid UserId = Guid.Parse("3e744b43-4e89-42d2-83c0-46b056dc5a0d");

    }
    public static class Util
    {
        public static string GetCartHashKey(string userId) => $"cart:{userId}";
    }
}
