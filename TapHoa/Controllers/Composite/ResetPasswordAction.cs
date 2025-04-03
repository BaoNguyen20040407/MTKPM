using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;

namespace TapHoa.Controllers.Composite
{
    public class ResetPasswordAction : IUserAction
    {
        private TapHoaEntities _db;

        public ResetPasswordAction(TapHoaEntities db)
        {
            _db = db;
        }

        public void Execute(NHANVIEN user)
        {
            var existingUser = _db.NHANVIENs.FirstOrDefault(x => x.TENDANGNHAP == user.TENDANGNHAP);
            if (existingUser != null)
            {
                existingUser.MATKHAU = "a1111111"; // Reset password về mặc định
                _db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Tên đăng nhập không hợp lệ hoặc đã hết hạn.");
            }
        }
    }
}
