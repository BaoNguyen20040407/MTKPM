using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TapHoa.Models;

namespace TapHoa.Controllers
{
    [RoutePrefix("banhang")]
    public class BanHangController : Controller
    {
        private readonly TapHoaEntities _db;

        //Khai báo để sử dụng Repository Pattern.
        private readonly HoaDonRepository _hoaDonRepo;

        //Khai báo để sử dụng Factory Method Pattern.
        private readonly IHoaDonFactory _hoaDonFactory;

        public BanHangController()
        {
            _db = new TapHoaEntities();
            _hoaDonRepo = new HoaDonRepository(_db);
            _hoaDonFactory = new HoaDonFactory();
        }

        [HttpGet, Route("index")]
        public async Task<ActionResult> Index(string searchString)
        {
            var sanphams = _db.SANPHAMs.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                sanphams = sanphams.Where(sp => sp.TENSP.Contains(searchString));
            }
            return View(await sanphams.ToListAsync());
        }

        [HttpPost, Route("create-order")]
        public ActionResult CreateOrder(OrderData orderData)
        {
            if (orderData?.SanPhams == null || !orderData.SanPhams.Any())
            {
                return Json(new { success = false, message = "Không có sản phẩm nào được chọn." });
            }

            try
            {
                var nhanVien = Session["NHANVIEN"] as NHANVIEN;
                var admin = Session["ADMIN"] as ADMIN;

                if (nhanVien == null && admin == null)
                {
                    return Json(new { success = false, message = "Người dùng không hợp lệ." });
                }

                string manv = admin != null ? "A000" : nhanVien.MANV;
                var hoaDon = _hoaDonFactory.CreateHoaDon(manv, orderData); //Factory Method Pattern
                _hoaDonRepo.AddHoaDon(hoaDon); //Dùng repository để tạo hóa đơn trong DB
                int sohd = hoaDon.SOHD;

                foreach (var sp in orderData.SanPhams)
                {
                    var sanPhamDb = _db.SANPHAMs.FirstOrDefault(p => p.MASP == sp.IdSanPham);
                    if (sanPhamDb != null)
                    {
                        sanPhamDb.SOLUONGDABAN += sp.SoLuong;
                        sanPhamDb.SOLUONG -= sp.SoLuong;

                        var chiTiet = _hoaDonFactory.CreateChiTietHoaDon(sohd, sp); //Factory Method Pattern
                        _db.CTHDs.Add(chiTiet);
                    }
                }

                _db.SaveChanges();
                return Json(new { success = true, message = "Đơn hàng đã được tạo thành công.", orderId = sohd });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpGet, Route("print-receipt/{orderId}")]
        public ActionResult PrintReceipt(int orderId)
        {
            var order = _db.HOADONs.Include(h => h.CTHDs.Select(ct => ct.SANPHAM)).FirstOrDefault(o => o.SOHD == orderId);
            if (order == null)
            {
                return HttpNotFound("Không tìm thấy hóa đơn.");
            }

            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document(new Rectangle(226, 400), 10, 10, 10, 10);
                PdfWriter.GetInstance(document, stream).CloseStream = false;
                document.Open();

                string fontPath = Server.MapPath("~/assets/fonts/Roboto-Regular.ttf");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font = new Font(baseFont, 12, Font.NORMAL);

                document.Add(new Paragraph("Hóa đơn bán hàng", font));
                document.Add(new Paragraph($"Mã đơn hàng: {order.SOHD}", font));
                document.Add(new Paragraph($"Ngày: {order.NGHD}", font));
                document.Add(new Paragraph($"Nhân viên: {order.NHANVIEN?.HOTEN ?? "Không rõ"}", font));
                document.Add(new Paragraph(""));

                foreach (var item in order.CTHDs)
                {
                    document.Add(new Paragraph($"{item.SANPHAM.TENSP} - SL: {item.SL} - Giá: {item.DONGIA} VND", font));
                }

                document.Add(new Paragraph(""));
                document.Add(new Paragraph($"Tổng tiền: {order.TONGTIEN} VND", font));
                document.Add(new Paragraph($"Tiền thối: {order.TIENTRALAI} VND", font));

                document.Close();
                return File(stream.ToArray(), "application/pdf", "hoa_don.pdf");
            }
        }
    }
}