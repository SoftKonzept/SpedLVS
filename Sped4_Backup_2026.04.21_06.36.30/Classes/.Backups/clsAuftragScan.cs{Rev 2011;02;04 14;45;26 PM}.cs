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
using TwainLib;
using TwainGui;
using System.Runtime.Serialization.Formatters.Binary;



namespace Sped4.Classes
{
  class clsAuftragScan
  {

    //************  User  ***************
    private Int32 _BenutzerID;
    public Int32 BenutzerID
    {
      get { return _BenutzerID; }
      set { _BenutzerID = value; }
    }
    //************************************#

    public clsImages img = new clsImages();
    private bool _m_bo_ScanOK;

    private Int32 _m_i_AuftragID;
    private Int32 _m_i_AuftragPos;
    private string _m_str_SavePathAndFilename;
    private string _m_str_Filename;
    private string _m_str_SavePath;
    private Int32 _m_i_picnum;
    private Int32 _ID;
    public byte[] bImage { get; set; }
    public Image _AuftragImageIn;
    public Image _AuftragImageOut;
    public Image _Thumb;
    public string m_str_ImageArt { get; set; }

    public Int32 ID
    {
      get { return _ID; }
      set { _ID = value; }
    }
    public Int32 m_i_AuftragID
    {
      get { return _m_i_AuftragID; }
      set { _m_i_AuftragID = value; }
    }
    public Int32 m_i_AuftragPos
    {
      get { return _m_i_AuftragPos; }
      set { _m_i_AuftragPos = value; }
    }
    public string m_str_Filename
    {
      get
      {
        _m_str_Filename = m_i_AuftragID.ToString() + "_" + m_i_picnum.ToString() + ".jpg";
        return _m_str_Filename;
      }
      set { _m_str_Filename = value; }
    }
    public string m_str_SavePathAndFilename
    {
      get
      {
        _m_str_SavePathAndFilename = m_str_SavePath + m_str_Filename;
        return _m_str_SavePathAndFilename;
      }
      set { _m_str_SavePathAndFilename = value; }
    }
    public string m_str_SavePath
    {
      get
      { 
        _m_str_SavePath=Application.StartupPath+"\\IMAGES\\";
        //Prüf ob Pfad / Ordner vorhanden und legt notfalls den Ordner an
        if(!Directory.Exists(_m_str_SavePath))
        {
          DirectoryInfo di = new DirectoryInfo(@_m_str_SavePath);
          di.Create();
        }
        
        return _m_str_SavePath;
      }
      set { _m_str_SavePath = value; }
    }
    public Int32 m_i_picnum
    {
      get { return _m_i_picnum; }
      set { _m_i_picnum = value; }
    }
    public Image Thumb
    {
      get 
      {
        _Thumb = Generate75x75Pixel((Bitmap)AuftragImageOut);
        return _Thumb;
      }
      set { _Thumb = value; }
    }
    public Image AuftragImageOut
    {
      get
      {
        _AuftragImageOut = byteArrayToImage(bImage);
        return _AuftragImageOut;
      }
      set { _AuftragImageOut = value; }
    }
    public Image AuftragImageIn
    {
      get { return _AuftragImageIn; }
      set { _AuftragImageIn = value; }
    }
    public bool m_bo_ScanOK
    {
      get { return _m_bo_ScanOK; }
      set { _m_bo_ScanOK = value; }
    }

    //+++++++++++++++++++++++++ Methoden  ------------------------------
    //
    //
    public void StartScan()
    {
      if (Functions.IsFormAlreadyOpen(typeof(MainFrame)) != null)
      {
        Functions.FormClose(typeof(MainFrame));
      }
      MainFrame ScanAction = new MainFrame(m_i_AuftragID);
      ScanAction.Show();
      ScanAction.BringToFront();
    }
    //
    //
    //
    public static DataTable GetAuftragImageTable(Int32 AuftragID)
    {
      DataTable dataTable = new DataTable();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;

      Command.CommandText = "Select ID, AuftragID, AuftragImage, ScanFilename, PicNum, ImageArt FROM AuftragScan WHERE AuftragID='" + AuftragID + "'";
      
      Globals.SQLcon.Open();
      ada.Fill(dataTable);
      Command.Dispose();
      Globals.SQLcon.Close();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }

      return dataTable;
    }
    //
    //-------- Read one Image ------------------
    //
    public byte[] ReadImage()
    {
      byte[] ImageByte;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;

      Command.CommandText = "Select AuftragImage FROM AuftragScan "+
                                                    "WHERE "+
                                                    "AuftragID='" + m_i_AuftragID + "' AND "+ 
                                                    "PicNum='"+m_i_picnum+"' AND " + 
                                                    "ImageArt='"+m_str_ImageArt+"'";

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();

      if (obj is DBNull)
      {
        ImageByte = null;
      }
      else
      {
        ImageByte = (byte[])obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }

      return ImageByte;     
    }
    //
    //--------- Create Thumb ------------
    //
    public bool ThumbnailCallback()
    {
      return false;
    }
    //
    //
    private Image MakeThumbnail(MemoryStream mem)
    {
      Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
      //Bitmap myBitmap = new Bitmap("Climber.jpg");
      mem.Seek(0, 0);
      Bitmap myBitmap = new Bitmap(mem);
      Image myThumbnail = myBitmap.GetThumbnailImage(75, 75, myCallback, IntPtr.Zero);
      //e.Graphics.DrawImage(myThumbnail, 150, 75);
      return myThumbnail;
    }
    //
    //
    private Image Generate75x75Pixel(Bitmap image)
    {
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
      Image img = (Image)bmp;
      return img;
    }
    //
    //
    //
    private Image byteArrayToImage(byte[] byteArrayIn)
    {
      MemoryStream ms = new MemoryStream(byteArrayIn);
      ms.Seek(0, 0);
      Image returnImage = Image.FromStream(ms);
      return returnImage;
    }
    //
    //
    //
    public void WriteToAuftragImage()
    {
      //AuftragImageIn.Save("C:\\gespeicher.jpg");

      if (AuftragImageIn != null)
      {
        MemoryStream mem = new MemoryStream();

        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(mem, AuftragImageIn);
        mem.Seek(0, 0);
        AuftragImageIn.Save(mem, ImageFormat.Jpeg);
        Int32 intLength = Convert.ToInt32(mem.Length.ToString());
        mem.Seek(0, 0);
        byte[] imgArr = new byte[intLength];
        mem.Read(imgArr, 0, intLength); ;
        mem.Close();

        string strSQL = "INSERT INTO AuftragScan " +
                                                 "(AuftragID, AuftragImage, ScanFilename, PicNum, ImageArt) " +
                                                 "VALUES ('" + m_i_AuftragID + "', " +
                                                          "@p, '" +
                                                          m_str_Filename + "', '" +
                                                          m_i_picnum + "', '" +
                                                          m_str_ImageArt + "')";


        InsertImage(strSQL, imgArr, "@p");
        AuftragImageIn.Dispose();
      }
    }
    //
    //--------------------- Update gedrehtes Image / Dokument -------------------
    //
    public void UpdateImage()
    {
      if (AuftragImageIn != null)
      {
        MemoryStream mem = new MemoryStream();

        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(mem, AuftragImageIn);
        mem.Seek(0, 0);
        AuftragImageIn.Save(mem, ImageFormat.Jpeg);
        Int32 intLength = Convert.ToInt32(mem.Length.ToString());
        mem.Seek(0, 0);
        byte[] imgArr = new byte[intLength];
        mem.Read(imgArr, 0, intLength); ;
        mem.Close();

        string strSQL = "Update AuftragScan Set AuftragImage =@p WHERE ID ='" + ID + "'";
        InsertImage(strSQL, imgArr, "@p");
        AuftragImageIn.Dispose();
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
    //--------------  05.7.2010 kann raus (beide) aber noch testen ---------------------------
    //InsertScanData und InsertPfad
    public void InsertScanData()
    {
      if (!CheckDoubleInsert())   // Prüft, ob Auftrag Scan schon vorhanden
      {
        InsertPfad();
      }
    }
    //
    //---------------------------- Schreibt den Pfad in die Datenbank
    //
    private void InsertPfad()
    {
      try
      {
        SqlCommand InsertCommand = new SqlCommand();
        InsertCommand.Connection = Globals.SQLcon.Connection;
        InsertCommand.CommandText = "INSERT INTO AuftragScan " +
                                                 "(AuftragID, Pfad, ScanFilename, PicNum) " +
                                                 "VALUES (" + m_i_AuftragID+ ", '" + m_str_SavePath + "', '" + m_str_Filename + "', " + m_i_picnum + ")";

        Globals.SQLcon.Open();
        InsertCommand.ExecuteNonQuery();
        InsertCommand.Dispose();
        Globals.SQLcon.Close();
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.ToString());
      }
    }
    //
    //----------------- Prüft doppelte Eintrag in der Datenbank -----------------------
    //
    private bool CheckDoubleInsert()
    {
      bool IsIn = true;
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Select ID FROM AuftragScan WHERE AuftragID='" + m_i_AuftragID + "' AND PicNum='" + m_i_picnum + "'";
      Globals.SQLcon.Open();
      object result = Command.ExecuteScalar();
      if (result == null)
      {
        IsIn = false;
      }
      else
      {
        Int32 ID = (Int32)Command.ExecuteScalar();
      }

      Command.Dispose();
      Globals.SQLcon.Close();
      return IsIn;
    }
    //
    //-------------- Check ob schon Dokumente vorhanden sind --------------------
    //
    public bool CheckAuftragScanIsIn()
    {
      bool IsIn = false;
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;

      Command.CommandText = "Select ID FROM AuftragScan WHERE AuftragID='" + m_i_AuftragID + "'";
      Globals.SQLcon.Open();
      if (Command.ExecuteScalar() == null)
      {
        IsIn = false;
      }
      else
      {
        Int32 ID = (Int32)Command.ExecuteScalar();
        IsIn = true;
      }
      //
      //Prüft of File existiert


      Command.Dispose();
      Globals.SQLcon.Close();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }
      return IsIn;
    }
    //
    //-------- ermitteln der PicNum der bereits vorliegenden Dokumente ------------------
    //
    public Int32 GetMaxPicNumByAuftrag()
    {
      Int32 maxPicNum = 0;
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Select MAX(PicNum) FROM AuftragScan WHERE AuftragID='" + m_i_AuftragID + "'";
      Globals.SQLcon.Open();

      object obj = Command.ExecuteScalar();

      if (obj is DBNull)
      {
          maxPicNum = 0;
      }
      else
      {
        maxPicNum = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }
      return maxPicNum +1;
    }
    //
    //---------------- Image  / Bilder löschen ---------------------
    //
    public static void DeleteAuftragScan(Int32 AuftragID, string ImageArt)
    {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "DELETE FROM AuftragScan WHERE AuftragID='" +AuftragID + "' AND ImageArt='"+ImageArt+"'";
        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();
        Command.Dispose();
        Globals.SQLcon.Close();
        if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
        {
          Command.Connection.Close();
        }
    }
    //
    //---------------- Image  / Bilder löschen ---------------------
    //
    public void DeleteAuftragScanByAuftragScanID()
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "DELETE FROM AuftragScan WHERE ID='" + ID + "'";
        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();
        Command.Dispose();
        Globals.SQLcon.Close();
        if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
        {
          Command.Connection.Close();
        }
      }
      catch (Exception ex)
      {
        //System.Windows.Forms.MessageBox.Show(ex.ToString());
        //Add Logbucheintrag Exception
        string Beschreibung = "Exception: " + ex;
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
      }
      finally
      {
        //Add Logbucheintrag Eintrag
        string Beschreibung = "Dokument: Auftrag "+m_i_AuftragID+"/"+m_i_AuftragPos+" - "+m_str_ImageArt+ "wurde gelöscht!";
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
      }
    }


  }
}
