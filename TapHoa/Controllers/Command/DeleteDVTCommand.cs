using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapHoa.Models;

namespace TapHoa.Controllers
{
    public class DeleteDVTCommand : ICommand
    {
        private readonly TapHoaEntities _db;
        private readonly string _id;

        public DeleteDVTCommand(TapHoaEntities db, string id)
        {
            _db = db;
            _id = id;
        }

        public void Execute()
        {
            var dvt = _db.DVTs.Find(_id);
            if (dvt != null)
            {
                _db.DVTs.Remove(dvt);
                _db.SaveChanges();
            }
        }
    }

}
