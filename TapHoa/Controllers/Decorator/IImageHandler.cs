using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TapHoa.Controllers
{
    // Interface Component
    public interface IImageHandler
    {
        string ProcessImage(HttpPostedFileBase image, string productId);
    }
}
