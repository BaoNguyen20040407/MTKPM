using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TapHoa.Models;

namespace TapHoa.Controllers.Memento
{
    public class CareTaker
    {
        private List<Memento> mementos = new List<Memento>();
        public SANPHAM sanPham;
        public int count = 1;
        public CareTaker(SANPHAM sanpham)
        {
            this.sanPham = sanpham;
            mementos.Add(new Memento(sanPham.TENSP, sanPham.GIAHIENHANH, sanPham.SOLUONG, sanPham.MADVT, sanPham.MALOAI, sanPham.HINHANH));
        }
        public void AddMemento(Memento memento)
        {
            mementos.Add(memento);
            count++;
        }
        public Memento Get(int index)
        {
            return mementos[index];
        }
        public Memento GetLast()
        {
            if (count > 1)
            {
                return mementos[count - 2];
            }
            return mementos[count - 1];
        }
    }
}