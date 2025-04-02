using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TapHoa.Models;

namespace TapHoa.Controllers.Memento
{
    public class CareTakerList
    {
        private List<CareTaker> careTakers = new List<CareTaker>();
        public CareTakerList() { }
        public void AddCareTaker(CareTaker careTaker)
        {
            careTakers.Add(careTaker);
        }
        public CareTaker Get(SANPHAM sanPham)
        {
            foreach (var item in careTakers)
            {
                if (item.sanPham.MASP == sanPham.MASP)
                {
                    return item;
                }
            }
            return null;
        }
    }
}