using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;

namespace TapHoa.Controllers.Composite
{
    public class UserActionComposite : IUserAction
    {
        private readonly List<IUserAction> _actions = new List<IUserAction>();

        public void Add(IUserAction action)
        {
            _actions.Add(action);
        }

        public void Remove(IUserAction action)
        {
            _actions.Remove(action);
        }

        public void Execute(NHANVIEN cust)
        {
            foreach (var action in _actions)
            {
                action.Execute(cust);
            }
        }
    }
}
