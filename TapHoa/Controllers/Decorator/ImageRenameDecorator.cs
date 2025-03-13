using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TapHoa.Controllers
{
    // Concrete Decorator - Rename Image
    public class ImageRenameDecorator : ImageHandlerDecorator
    {
        public ImageRenameDecorator(IImageHandler handler) : base(handler) { }

        public override string ProcessImage(HttpPostedFileBase image, string productId)
        {
            if (image == null || string.IsNullOrWhiteSpace(productId)) return null;

            string extension = Path.GetExtension(image.FileName);
            string newFilename = $"{SanitizeFileName(productId)}{extension}";

            string uploadPath = HttpContext.Current.Server.MapPath("~/Images");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string path = Path.Combine(uploadPath, newFilename);
            image.SaveAs(path);
            return newFilename;
        }

        private string SanitizeFileName(string input)
        {
            return string.Concat(input.Where(c => char.IsLetterOrDigit(c) || c == '_'));
        }
    }

}
