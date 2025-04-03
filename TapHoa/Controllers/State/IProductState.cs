using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapHoa.Controllers.State
{
    public interface IProductState
    {
        string GetState(int quantity);
    }
}
