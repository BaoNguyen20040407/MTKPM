using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TapHoa.Models;

namespace TapHoa.Controllers
{
    public class DVTController : Controller
    {
        private readonly TapHoaEntities _db = new TapHoaEntities();

        //Khai báo Invoker để thực hiện Command Pattern
        private readonly Invoker _invoker = new Invoker();

        public ActionResult Index()
        {
            return View(_db.DVTs.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TENDVT")] DVT dvt)
        {
            if (ModelState.IsValid)
            {
                //Sử dụng Command Pattern
                var command = new CreateDVTCommand(_db, dvt);
                _invoker.SetCommand(command);
                _invoker.ExecuteCommand();
                return RedirectToAction("Index");
            }
            return View(dvt);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var dvt = _db.DVTs.Find(id);
            if (dvt == null)
                return HttpNotFound();
            return View(dvt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MADVT,TENDVT")] DVT dvt)
        {
            if (ModelState.IsValid)
            {
                //Sử dụng Command Pattern
                var command = new EditDVTCommand(_db, dvt);
                _invoker.SetCommand(command);
                _invoker.ExecuteCommand();
                return RedirectToAction("Index");
            }
            return View(dvt);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var dvt = _db.DVTs.Find(id);
            if (dvt == null)
                return HttpNotFound();
            return View(dvt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                var command = new DeleteDVTCommand(_db, id);
                _invoker.SetCommand(command);
                _invoker.ExecuteCommand();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Không thể xóa do liên quan đến bảng khác");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
