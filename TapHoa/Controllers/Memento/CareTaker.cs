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
        public int count = 0;
        //public CareTaker(SANPHAM sanpham)
        //{
        //    this.sanPham = sanpham;
        //}
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
            if (count > 0)
            {
                count--;
            }
            return mementos[count];
        }
    }
}