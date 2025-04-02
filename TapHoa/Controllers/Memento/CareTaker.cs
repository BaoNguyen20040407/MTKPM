using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TapHoa.Controllers.Memento
{
    public class CareTaker
    {
        private List<Memento> mementos = new List<Memento>();
        public int count = 0;
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
                count--;
            }
            return mementos[count - 1];
        }
    }
}