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
        private string maNv;
        private string hoTen;
        private string dChi;
        private string sdt;
        private string tenDangNhap;
        private string matKhau;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Memento(string maNv, string hoTen, string dChi, string sdt, string tenDangNhap, string matKhau)
        {
            this.HOADONs = new HashSet<HOADON>();
            this.maNv = maNv;
            this.hoTen = hoTen;
            this.dChi = dChi;
            this.sdt = sdt;
            this.tenDangNhap = tenDangNhap;
            this.matKhau = matKhau;
        }

        public string MANV { get; set; }
        public string HOTEN { get; set; }
        public string DCHI { get; set; }
        public string SDT { get; set; }
        public string TENDANGNHAP { get; set; }
        public string MATKHAU { get; set; }
        public List<string> notifications = new List<string>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
        public void addNotify(string notify)
        {
            this.notifications.Add(notify);
        }
    }
}