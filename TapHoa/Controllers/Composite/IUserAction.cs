using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;

namespace TapHoa.Controllers.Composite
{
    public interface IUserAction
    {
        void Execute(NHANVIEN cust);
    }
}
