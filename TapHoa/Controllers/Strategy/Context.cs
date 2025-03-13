using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;

namespace TapHoa.Controllers.Strategy
{
    public class Context
    {
        private IChoice _choice;

        public void SetChoice(IChoice choice)
        {
            _choice = choice;
        }

        public string ExecuteStrategy(TapHoaEntities db)
        {
            return _choice.GenerateNewCode(db);
        }
    }

}
