using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace MrKatsuWeb.Common
{
    public class StringHelper
    {
        public static string GenerateGuid(int lenght)
        {
            var newCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, lenght);
            return newCode;
        }
        public static string GetAmount(decimal price)
        {
            if (price > 0)
            {
                var cultureInfo = new CultureInfo("vi-VN");
                return price.ToString("#,0", cultureInfo);
            }
            return "Free";
        }
        public static string CreateSeoAlias(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return string.Empty;
            }

            // Bước 1: Chuyển đổi tiếng Việt thành không dấu
            title = RemoveDiacritics(title);

            // Bước 2: Chuyển về chữ thường
            title = title.ToLowerInvariant();

            // Bước 3: Loại bỏ ký tự không hợp lệ
            title = Regex.Replace(title, @"[^a-z0-9\s-]", string.Empty);

            // Bước 4: Thay thế khoảng trắng hoặc dấu gạch ngang liền nhau thành một dấu gạch ngang
            title = Regex.Replace(title, @"\s+", "-").Trim('-');

            // Bước 5: Trả về chuỗi chuẩn hóa
            return title;
        }
        private static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
