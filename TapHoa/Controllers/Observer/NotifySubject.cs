//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TapHoa.Models;
using TapHoa.Singleton;

namespace TapHoa.Controllers.Observer
{
    public class NotifySubject : ISubject
    {
        List<NHANVIEN> observers = DbSingleton.Instance.NHANVIENs.ToList();
        //List<NHANVIEN> observers = new List<NHANVIEN>();
        public void Notify(SANPHAM sanPham, NHANVIEN nhanvien)
        {
            foreach (var observer in observers)
            {
                observer.Update(sanPham, nhanvien);
            }
        }
        public void Register(NHANVIEN observer)
        {
            observers.Add(observer);
            //observers.AddRange(listNv);
        }
        public void Unregister(NHANVIEN observer)
        {
            observers.Remove(observer);
        }
    }
}