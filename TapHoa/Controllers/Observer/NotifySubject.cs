//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TapHoa.Models;

namespace TapHoa.Controllers.Observer
{
    public class NotifySubject : ISubject
    {
        List<IObserver> observers = new List<IObserver>();
        public void Notify(SANPHAM sanPham)
        {
            foreach (var observer in observers)
            {
                observer.Update(sanPham);
            }
        }
        public void Register(IObserver observer)
        {
            observers.Add(observer);
        }
        public void Unregister(IObserver observer)
        {
            observers.Remove(observer);
        }
    }
}