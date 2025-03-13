using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TapHoa.Controllers.Strategy;
using TapHoa.Models;

namespace TapHoa.Controllers
{
    public class LOAIHANGsController : Controller
    {
        private TapHoaEntities db = new TapHoaEntities();
        //Khai báo để sử dụng Strategy
        private Context _context = new Context();

        public ActionResult Index()
        {
            return View(db.LOAIHANGs.ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIHANG lOAIHANG = db.LOAIHANGs.Find(id);
            if (lOAIHANG == null)
            {
                return HttpNotFound();
            }
            return View(lOAIHANG);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MALOAI,TENLOAI")] LOAIHANG lOAIHANG)
        {
            if (ModelState.IsValid)
            {
                if (lOAIHANG.TENLOAI.Contains("Sữa"))
                {
                    _context.SetChoice(new FirstChoice());
                    lOAIHANG.MALOAI = _context.ExecuteStrategy(db);
                }
                else if (lOAIHANG.TENLOAI.Contains("Mì gói"))
                {
                    _context.SetChoice(new SecondChoice());
                    lOAIHANG.MALOAI = _context.ExecuteStrategy(db);
                }
                else
                {
                    // Tìm mã lớn nhất hiện tại trong DB
                    var lastCode = db.LOAIHANGs
                                    .Where(l => l.MALOAI.StartsWith("C"))
                                    .OrderByDescending(l => l.MALOAI)
                                    .Select(l => l.MALOAI)
                                    .FirstOrDefault();

                    if (lastCode == null)
                    {
                        lOAIHANG.MALOAI = "C000";
                    }
                    else
                    {
                        // Tăng số thứ tự lên
                        int number = int.Parse(lastCode.Substring(1)) + 1;
                        lOAIHANG.MALOAI = $"C{number:D3}";
                    }
                }

                db.LOAIHANGs.Add(lOAIHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOAIHANG);
        }


        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIHANG lOAIHANG = db.LOAIHANGs.Find(id);
            if (lOAIHANG == null)
            {
                return HttpNotFound();
            }
            return View(lOAIHANG);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MALOAI,TENLOAI")] LOAIHANG lOAIHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAIHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOAIHANG);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIHANG lOAIHANG = db.LOAIHANGs.Find(id);
            if (lOAIHANG == null)
            {
                return HttpNotFound();
            }
            return View(lOAIHANG);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                LOAIHANG loaihang = db.LOAIHANGs.Find(id);
                db.LOAIHANGs.Remove(loaihang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Không xóa được do có liên quan đến bảng khác");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
