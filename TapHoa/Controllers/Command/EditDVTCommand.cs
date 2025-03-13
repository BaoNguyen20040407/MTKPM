using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;

namespace TapHoa.Controllers
{
    public class EditDVTCommand : ICommand
    {
        private readonly TapHoaEntities _db;
        private readonly DVT _dvt;

        public EditDVTCommand(TapHoaEntities db, DVT dvt)
        {
            _db = db;
            _dvt = dvt;
        }

        public void Execute()
        {
            _db.Entry(_dvt).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
