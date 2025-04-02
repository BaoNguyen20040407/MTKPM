using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TapHoa.Controllers.Observer;

namespace TapHoa.Controllers.Singleton
{
    public sealed class ObserverSingleton
    {
        private static NotifySubject _instance = new NotifySubject();
        private ObserverSingleton() { }
        public static NotifySubject Instance => _instance;
    }
}