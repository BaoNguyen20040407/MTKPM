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
        public void Add(Memento memento)
        {
            mementos.Add(memento);
        }
        public Memento Get(int index)
        {
            return mementos[index];
        }
        public Memento GetLast()
        {
            mementos.RemoveAt(mementos.Count - 1);
            return mementos.Last();
        }
    }
}