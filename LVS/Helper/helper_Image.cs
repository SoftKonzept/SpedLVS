using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LVS.Helper
{
    public class helper_Image
    {
        public static System.Drawing.Size DefaultMaxSize()
        {
            return new System.Drawing.Size(800, 600);
        }
        public static System.Drawing.Size DefaultThumbSize()
        {
            return new System.Drawing.Size(100, 75);
        }

        public static float DefaultThumbHeight()
        {
            return helper_Image.DefaultThumbSize().Height;
        }
        public static float DefaultThumbWidth()
        {
            return helper_Image.DefaultThumbSize().Width;
        }

        public static byte[] StreamToByte(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static void SaveByteArrayToFileWithStaticMethod(byte[] data, string filePath)
        {
            try
            {
                helper_IOFile.CheckPath(filePath);
                if (helper_IOFile.CheckFile(filePath))
                {
                    File.Delete(filePath);
                }
                File.WriteAllBytes(filePath, data);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        /// <summary>
        ///             Byte Array to Image
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            ms.Seek(0, 0);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static MemoryStream ByteArrayToStream(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            ms.Seek(0, 0);
            return ms;
        }
        /// <summary>
        ///             Byte Array to Bitmap
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public static Bitmap ByteArrayToBitmap(byte[] byteArrayIn)
        {
            //MemoryStream ms = new MemoryStream(byteArrayIn);
            //ms.Seek(0, 0);
            //Image returnImage = Image.FromStream(ms);
            var bmp = new Bitmap(new MemoryStream(byteArrayIn));
            return bmp;
        }


        /// <summary>
        ///             Image to Byte Array
        /// </summary>
        /// <param name="mem"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mem"></param>
        /// <returns></returns>
        public static Image MakeThumbnail(MemoryStream mem)
        {
            //Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            //Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(helper_FileAndImage.ThumbnailCallback);
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(helper_Image.ThumbnailCallback);
            //Bitmap myBitmap = new Bitmap("Climber.jpg");
            mem.Seek(0, 0);
            Bitmap myBitmap = new Bitmap(mem);
            Image myThumbnail = myBitmap.GetThumbnailImage(75, 75, myCallback, IntPtr.Zero);
            //e.Graphics.DrawImage(myThumbnail, 150, 75);
            return myThumbnail;
        }
        public static byte[] MakeThumbnailByteArray(MemoryStream mem)
        {
            //Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            //Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(helper_FileAndImage.ThumbnailCallback);
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(helper_Image.ThumbnailCallback);
            //Bitmap myBitmap = new Bitmap("Climber.jpg");
            mem.Seek(0, 0);
            Bitmap myBitmap = new Bitmap(mem);
            Image myThumbnail = myBitmap.GetThumbnailImage(75, 75, myCallback, IntPtr.Zero);
            byte[] retBytes = Helper.helper_Image.ImageToByteArray(myThumbnail);
            return retBytes;
        }
        public static bool ThumbnailCallback()
        {
            return false;
        }
        /// <summary>
        ///             Generate Image 75x75
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Image Generate75x75Pixel(Bitmap image)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            Bitmap bmp = null;
            Bitmap crapped = null;
            Int32 x = 0, y = 0;
            double prop = 0;

            if (image.Width > 75)
            {
                // compute proportation
                prop = (double)image.Width / (double)image.Height;

                if (image.Width > image.Height)
                {
                    x = (Int32)Math.Round(75 * prop, 0);
                    y = 75;
                }
                else
                {
                    x = 75;
                    y = (Int32)Math.Round(75 / prop, 0);
                }

                bmp = new Bitmap((Image)image, new System.Drawing.Size(x, y));
                crapped = new Bitmap(75, 75);
                Graphics g = Graphics.FromImage(crapped);
                g.DrawImage(bmp,
                    new Rectangle(0, 0, 75, 75),
                    new Rectangle(0, 0, 75, 75),
                    GraphicsUnit.Pixel);
                bmp = crapped;
            }
            else
            {
                crapped = image;
            }
            Image img = (Image)bmp;
            return img;
        }

        public static SqlParameter CreateImageParameter(string myPName, Image myImageToDB)
        {
            MemoryStream mem = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(mem, myImageToDB);
            mem.Seek(0, 0);
            myImageToDB.Save(mem, ImageFormat.Jpeg);
            Int32 intLength = Convert.ToInt32(mem.Length.ToString());
            mem.Seek(0, 0);
            byte[] byteArrayInImage = new byte[intLength];
            mem.Read(byteArrayInImage, 0, intLength);
            mem.Close();
            SqlParameter p = new SqlParameter(myPName, SqlDbType.Binary);
            p.Value = byteArrayInImage;
            return p;
        }

        public static SqlParameter CreateByteParameter(string myPName, Byte[] myByteArray)
        {
            //MemoryStream mem = new MemoryStream();
            //BinaryFormatter formatter = new BinaryFormatter();
            //formatter.Serialize(mem, myImageToDB);
            //mem.Seek(0, 0);
            //myImageToDB.Save(mem, ImageFormat.Jpeg);
            //Int32 intLength = Convert.ToInt32(mem.Length.ToString());
            //mem.Seek(0, 0);
            //byte[] byteArrayInImage = new byte[intLength];
            //mem.Read(byteArrayInImage, 0, intLength);
            //mem.Close();
            SqlParameter p = new SqlParameter(myPName, SqlDbType.Binary);
            p.Value = myByteArray;
            return p;
        }

        public static Image ReadImageByStringPath(string strFilePath, bool myOriginalSize)
        {
            System.Drawing.Size maxSize = helper_Image.DefaultMaxSize();
            Image ImageIn = null;
            try
            {
                ImageIn = Image.FromFile(strFilePath);

                if (myOriginalSize)
                {
                    if (
                            (ImageIn.Size.Width > maxSize.Width) &&
                            (ImageIn.Size.Height > maxSize.Height)
                       )
                    {
                        //optimierung original image
                        ResizeImage(ImageIn, maxSize.Width, maxSize.Height, false);
                    }
                }
                //returnImage = (Image)ImageIn.Clone();
                //ImageIn.Save(@"C:\LVS\Export\TestOriginal.jpg");
                //if ((ImageIn.Size.Width > helper_Image.) && (ImageIn.Size.Height > helper_Image.const_DeafultMaxSize.Height))
                //{
                //    //optimierung original image
                //    ResizeImage(ImageIn, (decimal)helper_Image.const_DeafultMaxSize.Width, (decimal)helper_Image.const_DeafultMaxSize.Height, false);
                //}
                //returnImage.Save(@"C:\LVS\Export\TestResize.jpg");
                //thumb wird erstellt
                //ResizeImage((decimal)const_DefaultThumbSize.Width, (decimal)const_DefaultThumbSize.Height, true);
                //ThumbImage.Save(@"C:\LVS\Export\TestThumb.jpg");
            }
            catch (Exception ex)
            { }
            return ImageIn;
        }
        /// <summary>
        ///             Image to Thumb
        /// </summary>
        /// <param name="myImage"></param>
        /// <param name="myWidth"></param>
        /// <param name="myHeight"></param>
        /// <param name="ForThumb"></param>
        /// <returns></returns>
        public static Image ResizeImage(Image myImage, decimal myWidth, decimal myHeight, bool ForThumb)
        {
            Bitmap bmpInput = (Bitmap)myImage;
            Bitmap bmpOutput = new Bitmap((Int32)myWidth, (Int32)myHeight, PixelFormat.Format24bppRgb);
            Graphics gOutput = Graphics.FromImage(bmpOutput);
            Rectangle rectOutput = new Rectangle(0, 0, bmpOutput.Width, bmpOutput.Height);

            ImageAttributes ia = new ImageAttributes();
            ia.SetWrapMode(WrapMode.TileFlipXY);

            if (ForThumb)
            {
                gOutput.InterpolationMode = InterpolationMode.NearestNeighbor;
            }
            else
            {
                gOutput.InterpolationMode = InterpolationMode.High;
            }
            gOutput.PixelOffsetMode = PixelOffsetMode.Half;
            gOutput.CompositingMode = CompositingMode.SourceCopy;
            gOutput.DrawImage(bmpInput, rectOutput,
                               0, 0, bmpInput.Width, bmpInput.Height,
                               GraphicsUnit.Pixel, ia);
            return (Image)bmpOutput;

            //if (ForThumb)
            //{
            //    ThumbImage = (Image)bmpOutput;
            //}
            //else
            //{
            //    returnImage = (Image)bmpOutput;
            //}
        }

        public static Bitmap ConvertToBitmap(BitmapSource bitmapSource)
        {
            var width = bitmapSource.PixelWidth;
            var height = bitmapSource.PixelHeight;
            var stride = width * ((bitmapSource.Format.BitsPerPixel + 7) / 8);
            var memoryBlockPointer = Marshal.AllocHGlobal(height * stride);
            bitmapSource.CopyPixels(new Int32Rect(0, 0, width, height), memoryBlockPointer, height * stride, stride);
            var bitmap = new Bitmap(width, height, stride, PixelFormat.Format32bppPArgb, memoryBlockPointer);
            return bitmap;
        }
    }
}
