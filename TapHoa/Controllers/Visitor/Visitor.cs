using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TapHoa.Models;

namespace TapHoa.Controllers.Visitor
{
    class Visitor : IVisitor
    {
        public void Visit(SANPHAM sanpham)
        {
            Console.WriteLine("Visitor visited " + sanpham.GetType().Name);
        }
    }
}