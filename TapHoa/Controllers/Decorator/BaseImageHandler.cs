using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TapHoa.Controllers
{
    // Concrete Component
    public class BaseImageHandler : IImageHandler
    {
        public string ProcessImage(HttpPostedFileBase image, string productId)
        {
            if (image == null) return null;

            string uploadPath = HttpContext.Current.Server.MapPath("~/Images");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string filename = Path.GetFileName(image.FileName);
            string path = Path.Combine(uploadPath, filename);

            // Kiểm tra định dạng file
            if (!IsImageFile(filename))
            {
                throw new InvalidOperationException("File không hợp lệ.");
            }

            image.SaveAs(path);
            return filename;
        }

        private bool IsImageFile(string filename)
        {
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            return validExtensions.Contains(Path.GetExtension(filename).ToLower());
        }
    }
}
