using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;

namespace TapHoa.Controllers.Visitor
{
    interface IVisitor
    {
        void Visit(SANPHAM sanpham);
    }
}
