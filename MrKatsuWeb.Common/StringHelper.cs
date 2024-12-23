using System.Globalization;

namespace MrKatsuWeb.Common
{
    public class StringHelper
    {
        public static string GetAmount(decimal price)
        {
            if (price > 0)
            {
                var cultureInfo = new CultureInfo("vi-VN");
                return price.ToString("#,0", cultureInfo);
            }
            return "Free";
        }
    }
}
