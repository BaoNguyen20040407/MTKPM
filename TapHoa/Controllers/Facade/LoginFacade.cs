using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapHoa.Controllers.Facade
{
    public class LoginFacade
    {
        private readonly AdminAuthentication adminAuth = new AdminAuthentication();
        private readonly EmployeeAuthentication employeeAuth = new EmployeeAuthentication();
        private readonly SessionManager sessionManager = new SessionManager();

        public string Login(string username, string password, out object userSession, out string role)
        {
            // Chặn tài khoản đặc biệt
            if (username == "tiemtaphoa" && password == "taphoacuatui")
            {
                userSession = null;
                role = null;
                return "Có thể tên đăng nhập hoặc mật khẩu đã sai, vui lòng kiểm tra lại!";
            }

            // Xác thực ADMIN
            var admin = adminAuth.Authenticate(username, password);
            if (admin != null)
            {
                userSession = admin;
                role = "Admin";
                sessionManager.SetUserSession(admin, role);
                return null; // Đăng nhập thành công
            }

            // Xác thực NHÂN VIÊN
            var employee = employeeAuth.Authenticate(username, password);
            if (employee != null)
            {
                userSession = employee;
                role = "Employee";
                sessionManager.SetUserSession(employee, role);
                return null; // Đăng nhập thành công
            }

            // Sai tài khoản hoặc mật khẩu
            userSession = null;
            role = null;
            return "Có thể tên đăng nhập hoặc mật khẩu đã sai, vui lòng kiểm tra lại!";
        }

        public void Logout()
        {
            sessionManager.Logout();
        }
    }

}
