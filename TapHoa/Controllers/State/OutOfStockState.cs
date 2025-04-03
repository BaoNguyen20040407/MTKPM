using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapHoa.Controllers.State
{
    public class OutOfStockState : IProductState
    {
        public string GetState(int quantity)
        {
            return "HẾT";
        }
    }
}
