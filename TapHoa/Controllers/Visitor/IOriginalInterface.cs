using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapHoa.Controllers.Visitor
{
    interface IOriginalInterface
    {
        void Accept(IVisitor visitor);
    }
}
