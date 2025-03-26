//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TapHoa.Models;

namespace TapHoa.Controllers.Observer
{
    public class NhanVienObserve : IObserver
    {
        private NHANVIEN _nhanVien;

        public NhanVienObserve(NHANVIEN nhanVien)
        {
            _nhanVien = nhanVien;
        }

        public void Update(SANPHAM sanPham)
        {
            Console.WriteLine($"Nhan vien {_nhanVien.HOTEN} nhan thong bao san pham: {sanPham.TENSP} co su thay doi!");
        }
    }
}