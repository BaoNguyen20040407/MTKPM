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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Memento()
        {
            this.HOADONs = new HashSet<HOADON>();
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