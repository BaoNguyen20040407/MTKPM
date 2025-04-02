using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TapHoa.Models;

namespace TapHoa.Controllers.Memento
{
    public class Memento
    {
        private string tenSp;
        private Nullable<decimal> giaHienHanh;
        private Nullable<int> soLuong;
        private string maDVT;
        private string maLoai;
        private string hinhAnh;
        public Memento(string tenSp, decimal? giaHienHanh, int? soLuong, string maDVT, string maLoai, string hinhAnh)
        {
            this.tenSp = tenSp;
            this.giaHienHanh = giaHienHanh;
            this.soLuong = soLuong;
            this.maDVT = maDVT;
            this.maLoai = maLoai;
            this.hinhAnh = hinhAnh;
        }

        public string TENSP() { return tenSp; }
        public Nullable<decimal> GIAHIENHANH() { return giaHienHanh; }
        public Nullable<int> SOLUONG() { return soLuong; }
        public string MADVT() { return maDVT; }
        public string MALOAI() { return maLoai; }
        public string HINHANH() { return hinhAnh; }
    }
}