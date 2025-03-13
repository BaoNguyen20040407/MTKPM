using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TapHoa.Controllers
{
    // Abstract Decorator
    public abstract class ImageHandlerDecorator : IImageHandler
    {
        protected IImageHandler _handler;

        protected ImageHandlerDecorator(IImageHandler handler)
        {
            _handler = handler;
        }

        public virtual string ProcessImage(HttpPostedFileBase image, string productId)
        {
            return _handler.ProcessImage(image, productId);
        }
    }
}
