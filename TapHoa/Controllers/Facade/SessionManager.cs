using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TapHoa.Controllers.Facade
{
    public class SessionManager
    {
        public void SetUserSession(object user, string role)
        {
            HttpContext.Current.Session["User"] = user;
            HttpContext.Current.Session["Role"] = role;
        }

        public void Logout()
        {
            HttpContext.Current.Session.Clear();
        }
    }

}
