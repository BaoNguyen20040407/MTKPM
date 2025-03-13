using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;

namespace TapHoa.Controllers
{
    public class CreateDVTCommand : ICommand
    {
        private readonly TapHoaEntities _db;
        private readonly DVT _dvt;

        public CreateDVTCommand(TapHoaEntities db, DVT dvt)
        {
            _db = db;
            _dvt = dvt;
        }

        public void Execute()
        {
            _dvt.MADVT = GenerateNewMADVT();
            _db.DVTs.Add(_dvt);
            _db.SaveChanges();
        }

        private string GenerateNewMADVT()
        {
            var lastDVT = _db.DVTs.OrderByDescending(d => d.MADVT).FirstOrDefault();
            if (lastDVT == null)
            {
                return "A000";
            }
            string lastMADVT = lastDVT.MADVT;
            char letterPart = lastMADVT[0];
            int numericPart = int.Parse(lastMADVT.Substring(1)) + 1;
            if (numericPart > 999)
            {
                numericPart = 0;
                letterPart++;
                if (letterPart > 'Z') throw new InvalidOperationException("Hết mã để sử dụng.");
            }
            return letterPart + numericPart.ToString("D3");
        }
    }
}
