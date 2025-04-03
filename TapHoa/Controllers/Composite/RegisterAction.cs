using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TapHoa.Controllers.Composite;
using TapHoa.Models;
using TapHoa.Singleton;
using static System.Web.Razor.Parser.SyntaxConstants;

namespace TapHoa.Controllers
{

    public class RegisterAction : IUserAction
    {
        private readonly TapHoaEntities _db;

        public RegisterAction(TapHoaEntities db) // Constructor nhận db
        {
            _db = db;
        }

        public void Execute(NHANVIEN cust)
        {
            var existingUser = _db.NHANVIENs.FirstOrDefault(x => x.SDT == cust.SDT);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Số điện thoại đã được sử dụng.");
            }

            // Sinh mã tự động (đảm bảo phương thức này tồn tại)
            cust.MANV = GenerateNewMANV();

            _db.NHANVIENs.Add(cust);
            _db.SaveChanges();
        }

        private string GenerateNewMANV()
        {
            var lastNV = _db.NHANVIENs.OrderByDescending(n => n.MANV).FirstOrDefault();
            if (lastNV == null)
            {
                return "A000";
            }

            char letter = lastNV.MANV[0];
            int number = int.Parse(lastNV.MANV.Substring(1)) + 1;

            if (number > 999)
            {
                number = 0;
                letter++;
                if (letter > 'Z') throw new InvalidOperationException("Hết mã nhân viên.");
            }

            return $"{letter}{number:D3}";
        }
    }

}