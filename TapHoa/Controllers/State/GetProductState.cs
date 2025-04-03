using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Controllers;

namespace TapHoa.Controllers.State
{
    public class ProductStateController
    {
        public string GetProductState(int quantity)
        {
            IProductState state = quantity > 0 ? (IProductState)new AvailableState() : new OutOfStockState();
            return state.GetState(quantity);
        }
    }
}
