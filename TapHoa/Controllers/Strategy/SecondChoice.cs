using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;

namespace TapHoa.Controllers.Strategy
{
    public class SecondChoice : IChoice
    {
        public string GenerateNewCode(TapHoaEntities db)
        {
            var lastItem = db.LOAIHANGs
                .OrderByDescending(d => d.MALOAI)
                .FirstOrDefault();

            // Nếu bảng rỗng hoặc không có mã hợp lệ, bắt đầu từ 0001
            if (lastItem == null || string.IsNullOrWhiteSpace(lastItem.MALOAI))
            {
                return "0001";
            }

            string lastCode = lastItem.MALOAI.Trim();
            string numericPart = new string(lastCode.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(numericPart))
            {
                throw new FormatException($"MALOAI '{lastCode}' không hợp lệ.");
            }

            int lastNumber;
            if (!int.TryParse(numericPart, out lastNumber))
            {
                throw new FormatException($"MALOAI '{lastCode}' không thể chuyển thành số.");
            }

            string newCode = (lastNumber + 1).ToString().PadLeft(4, '0'); // Đảm bảo đúng 4 ký tự

            return newCode;
        }
    }
}
