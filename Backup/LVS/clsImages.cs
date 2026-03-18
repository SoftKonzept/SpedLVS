using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Windows.Forms;

namespace LVS
{
    public class clsImages
    {
        public Size const_DeafultMaxSize = new Size(800, 600);
        public Size const_DefaultThumbSize = new Size(100, 75);

        public byte[] byteArrayIn { get; set; }
        public Image returnImage { get; set; }
        public Image ThumbImage { get; set; }
        public Image ImageIn { get; set; }
        public double fImageSize { get; set; }
        public double fWidth { get; set; }
        public double fHeight { get; set; }
        private double _fDIN;





        /***************************************************************
         *                  Methoden und Proceduren
         * **************************************************************/
        ///<summary>clsImages / ReadImageByStringPath</summary>
        ///<remarks></remarks>
        public void ReadImageByStringPath(string strFilePath)
        {
            try
            {
                ImageIn = Image.FromFile(strFilePath);
                returnImage = (Image)ImageIn.Clone();
                //ImageIn.Save(@"C:\LVS\Export\TestOriginal.jpg");
                if ((ImageIn.Size.Width > const_DeafultMaxSize.Width) && (ImageIn.Size.Height > const_DeafultMaxSize.Height))
                {
                    //optimierung original image
                    ResizeImage((decimal)const_DeafultMaxSize.Width, (decimal)const_DeafultMaxSize.Height, false);
                }
                //returnImage.Save(@"C:\LVS\Export\TestResize.jpg");
                //thumb wird erstellt
                ResizeImage((decimal)const_DefaultThumbSize.Width, (decimal)const_DefaultThumbSize.Height, true);
                //ThumbImage.Save(@"C:\LVS\Export\TestThumb.jpg");
            }
            catch (Exception ex)
            { }
        }
        ///<summary>clsImages / WriteImageToHDD</summary>
        ///<remarks></remarks>
        public void WriteImageToHDD(string myFilePath, string myThumbPath)
        {
            System.Drawing.Imaging.Encoder myEncoder;
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            Int32 quality = 50;
            //get the jpeg codec
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            //create an encoder parameter for the image quality
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            //create a collection of all parameters that we will pass to the encoder
            EncoderParameters encoderParams = new EncoderParameters(1);
            //set the quality parameter for the codec
            encoderParams.Param[0] = qualityParam;
            //save the image using the codec and the parameters
            //image.Save(path, jpegCodec, encoderParams);     
            // speichern in der Tmp Datei
            this.returnImage.Save(myFilePath, jpegCodec, encoderParams);
            this.ThumbImage.Save(myThumbPath, jpegCodec, encoderParams);
            //to PDF
            //var reportProzessor = new Telerik.Reporting.Processing.ReportProcessor();
            //var typeReportSource = new Telerik.Reporting.TypeReportSource();
            //typeReportSource.TypeName = "TestReport";
            //var result = reportProzessor.RenderReport("PDF", typeReportSource, null); 
        }
        ///<summary>clsImages / GetEncoderInfo</summary>
        ///<remarks></remarks> 
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        ///<summary>clsImages / ReadImageByStream</summary>
        ///<remarks></remarks>
        public void ReadImageByStream(Stream ms)
        {
            try
            {
                returnImage = Image.FromStream(ms);
                ImageIn = (Image)returnImage.Clone();
                GetThumbnails75X75();
            }
            catch (Exception ex)
            { }
        }
        ///<summary>clsImages / Copy</summary>
        ///<remarks></remarks>
        public clsImages Copy()
        {
            return (clsImages)this.MemberwiseClone();
        }
        ///<summary>clsImages / ConvertByteArrayToImage</summary>
        ///<remarks></remarks>
        public Image ConvertByteArrayToImage()
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            ms.Seek(0, 0);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        /////<summary>clsImages / ConvertByteArrayToImage</summary>
        /////<remarks></remarks>
        //public Image ConvertByteArrayToImage()
        //{
        //    MemoryStream ms = new MemoryStream(byteArrayIn);
        //    ms.Seek(0, 0);
        //    Image ImgOut = System.Drawing.Image.FromStream(ms);
        //    ms.Dispose();
        //    return ImgOut;

        //    //returnImage = Image.FromStream(ms);
        //    //return returnImage;
        //    //Bitmap newBitmap;
        //    ////using (MemoryStream memoryStream = new MemoryStream(byteArrayIn))
        //    ////    using (Image newImage = Image.FromStream(memoryStream))
        //    ////        newBitmap = new Bitmap(newImage);

        //    //ImageConverter ic = new ImageConverter();
        //    //newBitmap = (Bitmap)ic.ConvertTo(byteArrayIn, typeof(Bitmap));
        //    //return (Image)newBitmap;
        //}
        ///<summary>clsImages / byteArrayToImage</summary>
        ///<remarks></remarks>
        public void byteArrayToImage()
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            ms.Seek(0, 0);
            returnImage = Image.FromStream(ms);
        }
        ///<summary>clsImages / GetThumbnails75X75</summary>
        ///<remarks></remarks>
        private void GetThumbnails75X75()
        {
            Bitmap image = (Bitmap)ImageIn;
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

                bmp = new Bitmap((Image)image, new Size(x, y));
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
            //Image returnImage = (Image)bmp;
            ThumbImage = (Image)bmp;
        }
        ///<summary>clsImages / GetThumbnails75X75</summary>
        ///<remarks></remarks>
        public void CreateThumbnail(Int32 myWidth, Int32 myHeight)
        {
            Bitmap image = (Bitmap)ImageIn;
            if (image == null)
                throw new ArgumentNullException("image");

            Bitmap bmp = null;
            Bitmap crapped = null;
            Int32 x = 0, y = 0;
            double prop = 0;

            if (image.Width > myWidth)
            {
                // compute proportation
                prop = (double)image.Width / (double)image.Height;

                if (image.Width > image.Height)
                {
                    x = (Int32)Math.Round(myWidth * prop, 0);
                    y = myHeight;
                }
                else
                {
                    x = myWidth;
                    y = (Int32)Math.Round(myHeight / prop, 0);
                }

                bmp = new Bitmap((Image)image, new Size(x, y));
                crapped = new Bitmap(myWidth, myHeight);
                Graphics g = Graphics.FromImage(crapped);

                g.DrawImage(bmp,
                    new Rectangle(0, 0, myWidth, myHeight),
                    new Rectangle(0, 0, myWidth, myHeight),
                    GraphicsUnit.Pixel);
                bmp = crapped;
            }
            else
            {
                crapped = image;
            }
            //Image returnImage = (Image)bmp;
            ThumbImage = (Image)bmp;
        }
        ///<summary>clsImages / ResizeImage</summary>
        ///<remarks></remarks>
        public void ResizeImage(decimal myWidth, decimal myHeight, bool ForThumb)
        {
            Bitmap bmpInput = (Bitmap)ImageIn;
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

            if (ForThumb)
            {
                ThumbImage = (Image)bmpOutput;
            }
            else
            {
                returnImage = (Image)bmpOutput;
            }
        }
        ///<summary>clsImages / ThumbnailCallback</summary>
        ///<remarks></remarks>
        public bool ThumbnailCallback()
        {
            return false;
        }
        /******************************************************************************
         *  Info:
         *  Größe damit eine Seite / Image komplett angezeigt wird in AuftragView
         *  reduziert um Faktor 0,54
         *  Width: 656
         *  Height: 902
         *  ------
         *  Standard (100%)
         *  Widht: 1215
         *  Height: 1672
         *  
         * ****************************************************************************/

        //Faktor 29,7cm/21cm (Länge/Breite)1,4
        public double fDIN
        {
            get
            {
                _fDIN = 1.4;
                return _fDIN;
            }
            set
            {
                _fDIN = value;
            }

        }

        //
        //----------- Resize Image  ------------------------------
        //
        public void ResizeImageByOneFactor()
        {
            Image pImage = ImageIn;
            double factor = fImageSize;

            double newWidth = pImage.Width * factor;
            double newHeight = pImage.Height * factor;

            Bitmap newImage = new Bitmap((Convert.ToInt32(newWidth)), (Convert.ToInt32(newHeight)));

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(pImage, new Rectangle(0, 0, (Int32)newWidth, (Int32)newHeight));
            }
            returnImage = (Image)newImage;
        }
        //
        //----------- Resize Image  ------------------------------
        //
        public void ResizeImageByWidthHeight()
        {
            Image pImage = ImageIn;

            double newWidth = pImage.Width * fWidth;
            double newHeight = pImage.Height * fHeight;

            Bitmap newImage = new Bitmap((Convert.ToInt32(newWidth)), (Convert.ToInt32(newHeight)));

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(pImage, new Rectangle(0, 0, (Int32)newWidth, (Int32)newHeight));
            }
            returnImage = (Image)newImage;
        }
        //
        //----------- Resize Image  ------------------------------
        //
        public void ResizeImageByDIN()
        {
            Image pImage = ImageIn;

            double newWidth = pImage.Width / fDIN;
            double newHeight = pImage.Height / fDIN;

            Bitmap newImage = new Bitmap((Convert.ToInt32(newWidth)), (Convert.ToInt32(newHeight)));

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(pImage, new Rectangle(0, 0, (Int32)newWidth, (Int32)newHeight));
            }
            returnImage = (Image)newImage;
        }
        //
        //-------- Passbild Personal ---------------
        //
        public void WriteToPersonalImage(decimal _ID)
        {
            //ImageIn.Save("C:\\Bild.jpg");

            if (ImageIn != null)
            {
                MemoryStream mem = new MemoryStream();

                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(mem, ImageIn);
                mem.Seek(0, 0);
                ImageIn.Save(mem, ImageFormat.Jpeg);
                Int32 intLength = Convert.ToInt32(mem.Length.ToString());
                mem.Seek(0, 0);
                byte[] byteArrayIn = new byte[intLength];
                mem.Read(byteArrayIn, 0, intLength); ;
                mem.Close();

                string strSQL = "Update Personal SET Passbild =@p WHERE ID='" + _ID + "'";


                InsertImage(strSQL, byteArrayIn, "@p");
                ImageIn.Dispose();
            }
        }
        //
        //
        //
        private void InsertImage(string strSQL, byte[] arrObj, string strParameterName)
        {
            strSQL = strSQL.ToString().Trim();
            try
            {
                SqlCommand InsertCommand = new SqlCommand();
                InsertCommand.Connection = Globals.SQLcon.Connection;
                InsertCommand.CommandText = strSQL;
                InsertCommand.CommandType = CommandType.Text;
                InsertCommand.Parameters.Clear();

                SqlParameter p = new SqlParameter(strParameterName, SqlDbType.Binary);
                p.Value = arrObj;
                InsertCommand.Parameters.Add(p);

                Globals.SQLcon.Open();
                InsertCommand.ExecuteNonQuery();
                InsertCommand.Dispose();
                Globals.SQLcon.Close();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        //
        //
        //
        public static byte[] ImageToByteArray(Image ImageIn)
        {
            MemoryStream ms = new MemoryStream();
            ImageIn.Save(ms, ImageFormat.Bmp);
            return ms.ToArray();
        }

    }
}
