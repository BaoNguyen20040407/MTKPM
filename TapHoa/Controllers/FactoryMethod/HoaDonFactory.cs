using System;
using System.Linq;
using TapHoa.Models;

namespace TapHoa.Controllers
{
    public class HoaDonFactory : IHoaDonFactory
    {
        public HOADON CreateHoaDon(string manv, OrderData orderData)
        {
            return new HOADON
            {
                MANV = manv,
                NGHD = DateTime.Now,
                TONGTIEN = orderData.SanPhams.Sum(sp => sp.Gia * sp.SoLuong),
                TONGSL = orderData.SanPhams.Sum(sp => sp.SoLuong),
                PHAITRA = orderData.PhaiTra,
                TIENTRALAI = orderData.TienTraLai
            };
        }

        public HOADON CreateHoaDonHoanTra(string manv, OrderData orderData, int sohdGoc)
        {
            return new HOADON
            {
                SOHD = sohdGoc,  // Lưu giữ số hóa đơn gốc
                MANV = manv,
                NGHD = DateTime.Now,
                TONGTIEN = -orderData.SanPhams.Sum(sp => sp.Gia * sp.SoLuong),
                TONGSL = -orderData.SanPhams.Sum(sp => sp.SoLuong),
                PHAITRA = -orderData.PhaiTra,
                TIENTRALAI = -orderData.TienTraLai
            };
        }

        public CTHD CreateChiTietHoaDon(int sohd, DonHang sp)
        {
            return new CTHD
            {
                SOHD = sohd,
                MASP = sp.IdSanPham,
                SL = sp.SoLuong,
                DONGIA = sp.Gia
            };
        }
    }
}
