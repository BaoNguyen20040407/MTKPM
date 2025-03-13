using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;

namespace TapHoa.Controllers.Strategy
{
    public interface IChoice
    {
        string GenerateNewCode(TapHoaEntities db);
    }

}
