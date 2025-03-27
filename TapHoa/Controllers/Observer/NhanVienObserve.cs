//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TapHoa.Models;
using TapHoa.Singleton;

namespace TapHoa.Controllers.Observer
{
    public class NhanVienObserve : IObserver
    {
        private TapHoaEntities db = DbSingleton.Instance;
        private NHANVIEN _nhanVien;

        public NhanVienObserve(NHANVIEN nhanVien)
        {
            _nhanVien = nhanVien;
        }

        public void Update(SANPHAM sanPham)
        {
            foreach (var i in db.NHANVIENs.ToList())
            {
                i.addNotify($"Nhan vien {_nhanVien.HOTEN} nhan thong bao san pham: {sanPham.TENSP} co su thay doi!");
            }
            //_nhanVien.addNotify($"Nhan vien {_nhanVien.HOTEN} nhan thong bao san pham: {sanPham.TENSP} co su thay doi!");
            Console.WriteLine($"Nhan vien {_nhanVien.HOTEN} nhan thong bao san pham: {sanPham.TENSP} co su thay doi!");
        }
    }
}