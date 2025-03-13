using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;

namespace TapHoa.Controllers
{
    public interface IHoaDonFactory
    {
        HOADON CreateHoaDon(string manv, OrderData orderData);
        CTHD CreateChiTietHoaDon(int sohd, DonHang sp);
    }
}
