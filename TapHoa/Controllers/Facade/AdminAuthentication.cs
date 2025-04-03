using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;
using TapHoa.Singleton;

namespace TapHoa.Controllers.Facade
{
    public class AdminAuthentication
    {
        private readonly TapHoaEntities db = DbSingleton.Instance;

        public ADMIN Authenticate(string username, string password)
        {
            return db.ADMINs.FirstOrDefault(a => a.TENDANGNHAP == username && a.MATKHAU == password);
        }
    }

}
