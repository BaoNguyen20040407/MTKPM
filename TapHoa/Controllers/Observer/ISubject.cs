using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;

namespace TapHoa.Controllers.Observer
{
    public interface ISubject
    {
        void Register(NHANVIEN observer);
        void Unregister(NHANVIEN observer);
        void Notify(SANPHAM sanPham, NHANVIEN nhanvien);
    }
}
