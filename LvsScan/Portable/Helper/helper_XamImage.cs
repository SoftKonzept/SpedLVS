using Android.Graphics;
using System.IO;
using Xamarin.Forms;

namespace LvsScan.Portable.Helper
{
    public class helper_XamImage
    {
        public static Size DefaultMaxSize()
        {
            return new Size(1024, 768);  //new Size(800, 600);
        }
        public static double DefaultHeight()
        {
            return helper_XamImage.DefaultMaxSize().Height;
        }
        public static double DefaultWidth()
        {
            return helper_XamImage.DefaultMaxSize().Width;
        }

        public static Size DefaultThumbSize()
        {
            return new Size(75, 75);
        }
        public static double DefaultThumbHeight()
        {
            return helper_XamImage.DefaultThumbSize().Height;
        }
        public static double DefaultThumbWidth()
        {
            return helper_XamImage.DefaultThumbSize().Width;
        }
        public static byte[] XamImageToByteArray(Stream streamIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                streamIn.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static byte[] ResizeImageAndroid(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }

        public static byte[] ResizeImageAndroid(byte[] imageData)
        {
            int width = (int)helper_XamImage.DefaultWidth();
            int height = (int)helper_XamImage.DefaultHeight();
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            if (originalImage.Width > originalImage.Height)
            {
                //-Landscape
                width = (int)helper_XamImage.DefaultWidth();
                height = (int)helper_XamImage.DefaultHeight();
            }
            else
            {
                //-Portrait
                width = (int)helper_XamImage.DefaultHeight();
                height = (int)helper_XamImage.DefaultWidth();
            }

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }

        public static byte[] ResizeImageAndroidThumbnail(byte[] imageData)
        {
            int width = (int)helper_XamImage.DefaultThumbWidth();
            int height = (int)helper_XamImage.DefaultThumbHeight();
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);

            if (originalImage.Width > originalImage.Height)
            {
                //-Landscape
                width = (int)helper_XamImage.DefaultThumbWidth();
                height = (int)helper_XamImage.DefaultThumbHeight();
            }
            else
            {
                //-Portrait
                width = (int)helper_XamImage.DefaultThumbHeight();
                height = (int)helper_XamImage.DefaultThumbWidth();
            }

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }


        public static Bitmap ByteArrayToBitmap(byte[] byteArrayIn)
        {
            var bmp = BitmapFactory.DecodeByteArray(byteArrayIn, 0, byteArrayIn.Length);
            return bmp;
        }

        public static ImageSource ByteArrayToImageSource(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            // Erstelle einen MemoryStream aus dem Byte-Array
            using (var ms = new MemoryStream(byteArray))
            {
                return ImageSource.FromStream(() => ms);
            }
        }

    }
}
