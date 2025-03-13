using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;

namespace TapHoa.Controllers.Strategy
{
    public class FirstChoice : IChoice
    {
        public string GenerateNewCode(TapHoaEntities db)
        {
            var lastItem = db.LOAIHANGs.OrderByDescending(d => d.MALOAI).FirstOrDefault();
            if (lastItem == null)
            {
                return "A000";
            }

            string lastCode = lastItem.MALOAI;
            char letterPart = lastCode[0];
            int numericPart = int.Parse(lastCode.Substring(1));

            numericPart++;
            if (numericPart > 999)
            {
                numericPart = 0;
                letterPart++;
                if (letterPart > 'Z')
                {
                    throw new InvalidOperationException("Đã hết mã để sử dụng.");
                }
            }

            return letterPart + numericPart.ToString("D3");
        }
    }
}
