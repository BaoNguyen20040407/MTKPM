using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TapHoa.Controllers
{
    // Concrete Decorator - Compress Image
    public class ImageCompressionDecorator : ImageHandlerDecorator
    {
        private readonly long _quality;

        public ImageCompressionDecorator(IImageHandler handler, long quality = 70L) : base(handler)
        {
            _quality = quality;
        }

        public override string ProcessImage(HttpPostedFileBase image, string productId)
        {
            if (image == null) return null;

            string uploadPath = HttpContext.Current.Server.MapPath("~/Images");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string path = Path.Combine(uploadPath, $"{productId}.jpg");

            // Reset stream trước khi đọc
            image.InputStream.Position = 0;
            using (var img = Image.FromStream(image.InputStream))
            {
                var encoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                if (encoder == null) return null;

                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, _quality);

                img.Save(path, encoder, encoderParams);
            }

            return $"{productId}.jpg";
        }
    }

}
