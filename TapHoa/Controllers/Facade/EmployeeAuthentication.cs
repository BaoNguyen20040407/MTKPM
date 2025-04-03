using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;
using TapHoa.Singleton;

namespace TapHoa.Controllers.Facade
{
    public class EmployeeAuthentication
    {
        private readonly TapHoaEntities db = DbSingleton.Instance;

        public NHANVIEN Authenticate(string username, string password)
        {
            return db.NHANVIENs.FirstOrDefault(e => e.TENDANGNHAP == username && e.MATKHAU == password);
        }
    }

}
