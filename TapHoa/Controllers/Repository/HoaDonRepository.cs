using System.Linq;
using TapHoa.Models;

public class HoaDonRepository
{
    private TapHoaEntities _db;

    public HoaDonRepository(TapHoaEntities db)
    {
        _db = db;
    }

    public void AddHoaDon(HOADON hoaDon)
    {
        _db.HOADONs.Add(hoaDon);
        _db.SaveChanges();
    }

    public HOADON GetHoaDonById(int id)
    {
        return _db.HOADONs.Include("CTHDs.SANPHAM").FirstOrDefault(o => o.SOHD == id);
    }
}
