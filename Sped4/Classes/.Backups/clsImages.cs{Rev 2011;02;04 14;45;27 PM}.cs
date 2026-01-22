using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Sped4;
using Sped4.Classes;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sped4
{
  class clsImages
  {

    public byte[] byteArrayIn { get; set; }
    public Image returnImage { get; set; }
    public Image ImageIn { get; set; }
    public double fImageSize { get; set; }
    public double fWidth { get; set; }
    public double fHeight { get; set; }
    private double _fDIN;



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
    //
    public Image ConvertByteArrayToImage()
    {
        MemoryStream ms = new MemoryStream(byteArrayIn);
        ms.Seek(0, 0);
        Image returnImage = Image.FromStream(ms);
        return returnImage;
    }
      //
      //

    public void byteArrayToImage()
    {
      MemoryStream ms = new MemoryStream(byteArrayIn);
      ms.Seek(0, 0);
      Image returnImage = Image.FromStream(ms);
      //return returnImage;
    }
    //
    //-------------  Thumbnails
    //
    private void GetThumbnails75X75()
    {
      Bitmap image = (Bitmap)ImageIn;
      if (image == null)
        throw new ArgumentNullException("image");

      Bitmap bmp = null;
      Bitmap crapped = null;
      int x = 0, y = 0;
      double prop = 0;

      if (image.Width > 75)
      {
        // compute proportation
        prop = (double)image.Width / (double)image.Height;

        if (image.Width > image.Height)
        {
          x = (int)Math.Round(75 * prop, 0);
          y = 75;
        }
        else
        {
          x = 75;
          y = (int)Math.Round(75 / prop, 0);
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
      Image returnImage = (Image)bmp; 
    }
    public bool ThumbnailCallback()
    {
      return false;
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
        g.DrawImage(pImage, new Rectangle(0,0,(Int32)newWidth, (Int32)newHeight));
      }
      returnImage=(Image)newImage;
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
      double newHeight = pImage.Height/ fDIN;

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
    public void WriteToPersonalImage(Int32 _ID)
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

        string strSQL = "Update Personal SET Passbild =@p WHERE ID='"+_ID+"'";


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
        MessageBox.Show(ex.ToString());
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
