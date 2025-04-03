using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapHoa.Controllers.State
{
    public class AvailableState : IProductState
    {
        public string GetState(int quantity)
        {
            return quantity > 0 ? quantity.ToString() : "HẾT";
        }
    }

}
