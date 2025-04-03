using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TapHoa.Controllers.Composite;
using TapHoa.Controllers.Facade;
using TapHoa.Controllers.Memento;
using TapHoa.Controllers.Observer;
using TapHoa.Controllers.Singleton;
using TapHoa.Models;
using TapHoa.Singleton;

namespace TapHoa.Controllers
{
    public class UserController : Controller
    {
        // Sử dụng Singleton thay vì tạo mới TapHoaEntities
        private readonly TapHoaEntities db = DbSingleton.Instance;
        NotifySubject notifySubject = ObserverSingleton.Instance;

        // GET: Account
        public ActionResult Index()
        {
            return View(db.NHANVIENs.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MANV,HOTEN,DCHI,SDT,TENDANGNHAP,MATKHAU")] NHANVIEN nhanvien)
        {
            if (db.NHANVIENs.Any(x => x.SDT == nhanvien.SDT))
            {
                ModelState.AddModelError("SDT", "Số điện thoại đã được sử dụng. Vui lòng sử dụng số khác.");
            }
            if (!IsValidUsernameOrPassword(nhanvien.TENDANGNHAP))
            {
                ModelState.AddModelError("TENDANGNHAP", "Tên đăng nhập phải có ít nhất 8 ký tự, bao gồm chữ và số.");
            }
            if (!IsValidUsernameOrPassword(nhanvien.MATKHAU))
            {
                ModelState.AddModelError("MATKHAU", "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ và số.");
            }
            if (ModelState.IsValid)
            {
                nhanvien.MANV = GenerateNewMANV();
                db.NHANVIENs.Add(nhanvien);
                notifySubject.Register(nhanvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhanvien);
        }
        private bool IsValidUsernameOrPassword(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 8)
            {
                return false;
            }

            bool hasLetter = input.Any(char.IsLetter);
            bool hasDigit = input.Any(char.IsDigit);

            return hasLetter && hasDigit;
        }
        // GET: DVT/Edit/5
        public ActionResult Edit(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nhanvien = db.NHANVIENs.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // POST: DVT/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MANV,HOTEN,DCHI,SDT,TENDANGNHAP,MATKHAU")] NHANVIEN nhanvien)
        {
            if (db.NHANVIENs.Any(x => x.SDT == nhanvien.SDT))
            {
                ModelState.AddModelError("SDT", "Số điện thoại đã được sử dụng. Vui lòng sử dụng số khác.");
            }
            if (!IsValidUsernameOrPassword(nhanvien.TENDANGNHAP))
            {
                ModelState.AddModelError("TENDANGNHAP", "Tên đăng nhập phải có ít nhất 8 ký tự, bao gồm chữ và số.");
            }
            if (!IsValidUsernameOrPassword(nhanvien.MATKHAU))
            {
                ModelState.AddModelError("MATKHAU", "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ và số.");
            }
            if (ModelState.IsValid)
            {
                db.Entry(nhanvien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanvien);
        }

        // GET: DVT/Delete/5
        public ActionResult Delete(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nhanvien = db.NHANVIENs.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // POST: DVT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(String id)
        {

            try
            {
                NHANVIEN nhanvien = db.NHANVIENs.Find(id);
                db.NHANVIENs.Remove(nhanvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Không xóa được do có liên quan đến bảng khác");
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(NHANVIEN cust)
        {
            if (ModelState.IsValid)
            {
                LoginFacade loginFacade = new LoginFacade();
                object userSession;
                string role;
                string errorMessage = loginFacade.Login(cust.TENDANGNHAP, cust.MATKHAU, out userSession, out role);

                if (errorMessage == null) // Đăng nhập thành công
                {
                    if (role == "Admin")
                    {
                        Session["ADMIN"] = userSession;
                    }
                    else if (role == "Employee")
                    {
                        Session["NHANVIEN"] = userSession;
                    }
                    return RedirectToAction("Index", "Home");
                }

                // Đăng nhập thất bại
                ViewBag.LoginFail = errorMessage;
            }

            return View("Login");
        }
        private string GenerateNewMANV()
        {
            var lastDVT = db.NHANVIENs.OrderByDescending(d => d.MANV).FirstOrDefault();
            if (lastDVT == null)
            {
                return "A000"; // Nếu chưa có mã nào trong cơ sở dữ liệu
            }

            string lastMADVT = lastDVT.MANV;
            char letterPart = lastMADVT[0];
            int numericPart = int.Parse(lastMADVT.Substring(1));

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

            string newMADVT = letterPart + numericPart.ToString("D3"); // Đảm bảo có 3 chữ số
            return newMADVT;
        }
        [HttpPost]
        public JsonResult IsPhoneNumberAvailable(string phoneNumber)
        {
            var existingUser = db.NHANVIENs.FirstOrDefault(x => x.SDT == phoneNumber);
            return Json(existingUser == null);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(NHANVIEN cust)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var db = DbSingleton.Instance;

                    // Tạo các hành động đăng ký
                    var registerAction = new RegisterAction(db);

                    // Composite Pattern để gộp nhiều hành động (mở rộng sau này)
                    var userActionComposite = new UserActionComposite();
                    userActionComposite.Add(registerAction);

                    // Thực thi hành động
                    userActionComposite.Execute(cust);

                    return RedirectToAction("Login");
                }
                catch (InvalidOperationException ex)
                {
                    ViewBag.RegisterFail = ex.Message;
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["ADMIN"] = null;
            Session["NHANVIEN"] = null;
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult ResetPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPass(NHANVIEN cust)
        {
            if (cust.TENDANGNHAP == "tiemtaphoa")
            {
                ViewBag.Message = "Không thể đặt lại mật khẩu cho tài khoản hệ thống.";
                return View();
            }

            try
            {
                // Tạo một instance của Composite để chứa nhiều hành động
                UserActionComposite composite = new UserActionComposite();

                // Thêm hành động ResetPassword vào Composite
                composite.Add(new ResetPasswordAction(db));

                // Thực thi tất cả hành động
                composite.Execute(cust);

                ViewBag.Message = "Mật khẩu đã được đặt lại về mặc định.";
                return RedirectToAction("Login", "User");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

    }
}