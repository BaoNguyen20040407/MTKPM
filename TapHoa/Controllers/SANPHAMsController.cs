using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TapHoa.Controllers.Observer;
using TapHoa.Models;

namespace TapHoa.Controllers
{
    public class SANPHAMsController : Controller
    {
        private TapHoaEntities db = new TapHoaEntities();
        private readonly IImageHandler _imageHandler;
        public SANPHAMsController()
        {
            _imageHandler = new ImageCompressionDecorator(
                                new ImageRenameDecorator(
                                    new BaseImageHandler()
                                )
                            );
        }

        // GET: SANPHAMs
        public async Task<ActionResult> Index(string searchString)
        {
            if (db.SANPHAMs == null)
            {
                return Content("Không tìm thấy sản phẩm.");
            }

            var sanpham = from e in db.SANPHAMs select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                sanpham = sanpham.Where(a => a.TENSP.Contains(searchString));
            }

            return View(await sanpham.ToListAsync());
        }

        // GET: SANPHAMs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sanpham = db.SANPHAMs.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        private string GenerateNewMASP()
        {
            var lastProduct = db.SANPHAMs.OrderByDescending(d => d.MASP).FirstOrDefault();
            if (lastProduct == null)
            {
                return "A000";
            }

            string lastId = lastProduct.MASP;
            char letterPart = lastId[0];
            int numericPart = int.Parse(lastId.Substring(1)) + 1;

            if (numericPart > 999)
            {
                numericPart = 0;
                letterPart++;
                if (letterPart > 'Z')
                {
                    throw new InvalidOperationException("Hết mã sản phẩm.");
                }
            }

            return letterPart + numericPart.ToString("D3");
        }

        // GET: SANPHAMs/Create
        public ActionResult Create()
        {
            ViewBag.MADVT = new SelectList(db.DVTs, "MADVT", "TENDVT");
            ViewBag.MALOAI = new SelectList(db.LOAIHANGs, "MALOAI", "TENLOAI");
            return View();
        }

        // POST: SANPHAMs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MASP,TENSP,MADVT,HANGSX,GIAHIENHANH,SOLUONG,MALOAI")] SANPHAM sanpham, HttpPostedFileBase HINHANH)
        {
            if (sanpham.SOLUONG < 0)
            {
                ModelState.AddModelError("SOLUONG", "Số lượng không được âm.");
            }

            if (ModelState.IsValid)
            {
                sanpham.MASP = GenerateNewMASP();
                sanpham.SOLUONGDABAN = 0;

                // Xử lý ảnh bằng Decorator
                if (HINHANH != null)
                {
                    sanpham.HINHANH = _imageHandler.ProcessImage(HINHANH, sanpham.MASP);
                }

                db.SANPHAMs.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADVT = new SelectList(db.DVTs, "MADVT", "TENDVT", sanpham.MADVT);
            ViewBag.MALOAI = new SelectList(db.LOAIHANGs, "MALOAI", "TENLOAI", sanpham.MALOAI);
            return View(sanpham);
        }

        // GET: SANPHAMs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sanpham = db.SANPHAMs.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADVT = new SelectList(db.DVTs, "MADVT", "TENDVT", sanpham.MADVT);
            ViewBag.MALOAI = new SelectList(db.LOAIHANGs, "MALOAI", "TENLOAI", sanpham.MALOAI);
            return View(sanpham);
        }

        // POST: SANPHAMs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MASP,TENSP,MADVT,HANGSX,GIAHIENHANH,SOLUONG,MALOAI,HINHANH")] SANPHAM sanpham, HttpPostedFileBase HinhAnh)
        {
            NotifySubject notifySubject = new NotifySubject();
            if (sanpham.SOLUONG < 0)
            {
                ModelState.AddModelError("SOLUONG", "Số lượng không được âm.");
            }
            if (ModelState.IsValid)
            {
                var existingProduct = db.SANPHAMs.FirstOrDefault(p => p.MASP == sanpham.MASP);
                if (existingProduct != null)
                {
                    existingProduct.TENSP = sanpham.TENSP;
                    existingProduct.GIAHIENHANH = sanpham.GIAHIENHANH;
                    existingProduct.SOLUONG = sanpham.SOLUONG;
                    existingProduct.MADVT = sanpham.MADVT;
                    existingProduct.MALOAI = sanpham.MALOAI;

                    if (HinhAnh != null)
                    {
                        existingProduct.HINHANH = _imageHandler.ProcessImage(HinhAnh, existingProduct.MASP);
                    }
                }
                notifySubject.Notify(existingProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADVT = new SelectList(db.DVTs, "MADVT", "TENDVT", sanpham.MADVT);
            ViewBag.MALOAI = new SelectList(db.LOAIHANGs, "MALOAI", "TENLOAI", sanpham.MALOAI);
            return View(sanpham);
        }

        // DELETE: SANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                SANPHAM sanpham = db.SANPHAMs.Find(id);
                db.SANPHAMs.Remove(sanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Không xóa được do có liên quan đến bảng khác.");
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
