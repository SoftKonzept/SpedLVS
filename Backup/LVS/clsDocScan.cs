using Common.Enumerations;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Windows.Forms;

namespace LVS
{
    public class clsDocScan
    {
        //public ctrMenu _ctrMenu;
        //public frmAuftragView _frmAV;
        public Globals._GL_SYSTEM _GLSystem;
        public Globals._GL_USER _GL_User;
        public clsSystem sys;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }
        //************************************

        public clsImages img = new clsImages();
        private bool _m_bo_ScanOK;


        public decimal ID { get; set; }
        public decimal AuftragTableID { get; set; }
        public decimal LEingangTableID { get; set; }
        public decimal LAusgangTableID { get; set; }

        private string Path { get; set; }
        private Int32 Picnum { get; set; }

        public string Filename { get; set; }
        public string ImageArt { get; set; }
        public decimal AuftragPosTableID { get; set; }
        public Image DocImage { get; set; }
        public Image ImageIn { get; set; }
        public Image ImageOut { get; set; }
        public Image ImgThumb { get; set; }
        private string _DocPath;
        public string DocPath
        {
            get
            {
                _DocPath = this.Path + "\\" + this.Filename;
                return _DocPath;
            }
            set { _DocPath = value; }
        }
        public decimal TableID { get; set; }
        public string TableName { get; set; }
        public string StartupPath { get; set; }
        public bool IsForSPLMessage { get; set; }



        private decimal _m_dec_DocScanID;
        private decimal _m_dec_AuftragID;
        private decimal _m_dec_LEingangID;
        private decimal _m_dec_LAusgangID;
        private decimal _m_dec_AuftragPos;
        private decimal _m_dec_AuftragPosTableID;
        private string _m_str_SavePathAndFilename;
        private string _m_str_SavePathAndFilenamePDF;
        private string _m_str_SavePathAndFilenameTMP;
        private string _m_str_Filename;
        private string _m_str_FilenamePDF;
        private string _m_str_Path;
        private Int32 _m_i_picnum;
        private decimal _ID;
        private byte[] _bImage;
        public Image _AuftragImageIn;
        public Image _AuftragImageOut;
        public Image _Thumb;
        public string m_str_ImageArt { get; set; }
        public decimal m_dec_DocScanID { get; set; }
        public decimal m_dec_AuftragID { get; set; }
        public decimal m_dec_AuftragPos { get; set; }
        public decimal m_dec_LEingangID { get; set; }
        public decimal m_dec_LAusgangID { get; set; }
        public decimal m_dec_AuftragPosTableID { get; set; }
        public string m_str_Filename
        {
            get
            {
                _m_str_Filename = m_dec_DocScanID.ToString() + ".jpg";
                //_m_str_Filename = m_dec_DocScanID.ToString() + ".PDF";
                return _m_str_Filename;
            }
            set { _m_str_Filename = value; }
        }
        public string m_str_FilenamePDF
        {
            get
            {
                //_m_str_Filename = m_dec_DocScanID.ToString() + ".jpg";
                _m_str_FilenamePDF = m_dec_DocScanID.ToString() + ".PDF";
                return _m_str_FilenamePDF;
            }
            set { _m_str_FilenamePDF = value; }
        }
        public string m_str_SavePathAndFilename
        {
            get
            {
                _m_str_SavePathAndFilename = m_str_Path + "\\" + m_str_Filename;
                return _m_str_SavePathAndFilename;
            }
            set { _m_str_SavePathAndFilename = value; }
        }
        public string m_str_SavePathAndFilenamePDF
        {
            get
            {
                _m_str_SavePathAndFilenamePDF = m_str_Path + "\\" + m_str_FilenamePDF;
                return _m_str_SavePathAndFilenamePDF;
            }
            set { _m_str_SavePathAndFilenamePDF = value; }
        }
        public string m_str_SavePathAndFilenameTMP
        {
            get
            {
                _m_str_SavePathAndFilenameTMP = m_str_Path + "\\tmp" + m_str_Filename;
                return _m_str_SavePathAndFilenameTMP;
            }
            set { _m_str_SavePathAndFilenameTMP = value; }
        }

        public string m_str_Path
        {
            get
            {
                _m_str_Path = this._GLSystem.VE_DocScanPath;
                //Prüf ob Pfad / Ordner vorhanden und legt notfalls den Ordner an
                if (!Directory.Exists(_m_str_Path))
                {
                    DirectoryInfo di = new DirectoryInfo(@_m_str_Path);
                    di.Create();
                }

                return _m_str_Path;
            }
            set { _m_str_Path = value; }
        }

        public byte[] bImage
        {
            get
            {
                return _bImage;
            }
            set
            {
                //string myStrImagePfad = Application.StartupPath + m_str_Path + m_str_Filename;
                string myStrImagePfad = StartupPath + m_str_Path + m_str_Filename;
                //Pfadprüfen 
                if (File.Exists(myStrImagePfad))
                {
                    Bitmap bm = new Bitmap(myStrImagePfad);
                    MemoryStream ms = new MemoryStream();
                    bm.Save(ms, ImageFormat.Jpeg);
                    _bImage = ms.ToArray();
                }

                _bImage = value;
            }
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
                if (AuftragImageOut != null)
                {
                    _Thumb = Generate75x75Pixel((Bitmap)AuftragImageOut);
                }
                return _Thumb;
            }
            set { _Thumb = value; }
        }

        public static Image MakeThumb(Bitmap image)
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



        public Image AuftragImageOut
        {
            get
            {
                if (bImage != null)
                {
                    _AuftragImageOut = byteArrayToImage(bImage);
                }
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


        /************************************************************************************
         *                          Methoden
         * ********************************************************************************/
        ///<summary>clsDocScan / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, clsSystem mySystem)
        {
            this._GL_User = myGLUser;
            this._GLSystem = myGLSystem;
            this.sys = mySystem;
        }
        ///<summary>clsDocScan / Copy</summary>
        ///<remarks></remarks>
        public clsDocScan Copy()
        {
            return (clsDocScan)this.MemberwiseClone();
        }
        ///<summary>clsDocScan / Copy</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM DocScan WHERE ID=" + this.ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "DocScan");
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                this.ID = decTmp;
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["AuftragTableID"].ToString(), out decTmp);
                this.AuftragTableID = decTmp;
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["LEingangTableID"].ToString(), out decTmp);
                this.LEingangTableID = decTmp;
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["LAusgangTableID"].ToString(), out decTmp);
                this.LAusgangTableID = decTmp;
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["PicNum"].ToString(), out iTmp);
                this.Picnum = iTmp;
                this.Path = dt.Rows[i]["Pfad"].ToString();
                this.Filename = dt.Rows[i]["ScanFilename"].ToString();
                this.ImageArt = dt.Rows[i]["ImageArt"].ToString();
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["AuftragPosTableID"].ToString(), out decTmp);
                this.AuftragPosTableID = decTmp;
                this.TableName = dt.Rows[i]["TableName"].ToString();
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["TableID"].ToString(), out decTmp);
                this.TableID = decTmp;
                this.IsForSPLMessage = (bool)dt.Rows[i]["IsForSPLMessage"];

            }
        }
        ///<summary>clsAuftragScan / GetMaxPicNumByAuftrag</summary>
        ///<remarks>Ermittel anhand der AuftragsID die Anzahl der bereits gespeicherten 
        ///         Dokumente (PicNum).</remarks>
        ///<return>myIMaxPicNum</return>
        public void AddFromFileRead(clsImages myImages, string length)
        {
            this.img = myImages.Copy();
            try
            {
                if (this.img.ImageIn != null)
                {
                    Picnum = GetMaxPicNum(this.TableName, this.TableID);
                    string strParameterImage = "@pDocImage";
                    string strParameterThumb = "@pThumbnail";

                    string strSQL = "INSERT INTO DocScan " +
                                                 "(AuftragTableID, LEingangTableID, LAusgangTableID, Pfad, ScanFilename, PicNum, ImageArt, AuftragPosTableID " +
                                                 //", DocImage, TableName, TableID )"+//, Thumbnail) " +
                                                 ", DocImage, TableName, TableID , Thumbnail, IsForSPLMessage) " +
                                                 "VALUES (" + (Int32)AuftragTableID +
                                                       ", " + LEingangTableID +
                                                       ", " + LAusgangTableID +
                                                       ", '" + Path + "'" +
                                                       ", '" + Filename + "'" +
                                                       ", " + Picnum +
                                                       ", '" + ImageArt + "'" +
                                                       ", " + AuftragPosTableID +
                                                       ", " + strParameterImage +
                                                       ", '" + this.TableName + "' " +
                                                       ", " + (Int32)this.TableID +
                                                       ", " + strParameterThumb +
                                                       ", " + Convert.ToInt32(IsForSPLMessage) +
                                                       ")";

                    try
                    {
                        SqlCommand InsertCommand = new SqlCommand();
                        InsertCommand.Connection = Globals.SQLcon.Connection;
                        InsertCommand.CommandText = strSQL;
                        InsertCommand.CommandType = CommandType.Text;
                        InsertCommand.Parameters.Clear();
                        //Parameterübergabe

                        InsertCommand.Parameters.Add(CreateImageParameter(strParameterImage, myImages.returnImage));
                        InsertCommand.Parameters.Add(CreateImageParameter(strParameterThumb, myImages.ThumbImage));
                        Globals.SQLcon.Open();
                        InsertCommand.ExecuteNonQuery();
                        InsertCommand.Dispose();
                        Globals.SQLcon.Close();

                        if (this.TableName.Equals(enumDatabaseSped4_TableNames.Artikel.ToString()))
                        {
                            clsArtikelVita.AddImageToArtikel(this._GL_User, this.TableID, enumLagerAktionen.ImageToArtikel.ToString(), Filename);
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                        clsMail EMail = new clsMail();
                        EMail.InitClass(this._GL_User, this.sys);
                        EMail.Subject = this.sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error clsDocScan: Error beim Insert";
                        string strError = "Class-Daten:" + Environment.NewLine;
                        if (this.AuftragTableID == null)
                        {
                            strError = strError + "AuftragTableID: => NULL" + Environment.NewLine;
                        }
                        else
                        {
                            strError = strError + "AuftragTableID: " + this.AuftragTableID.ToString() + " | Length: " + this.AuftragTableID.ToString().Length.ToString() + Environment.NewLine;

                        }
                        if (this.LEingangTableID == null)
                        {
                            strError = strError + "LEingangTableID: => NULL " + Environment.NewLine;
                        }
                        else
                        {
                            strError = strError + "LEingangTableID: " + this.LEingangTableID.ToString() + length + this.LEingangTableID.ToString().Length.ToString() + Environment.NewLine;
                        }
                        if (this.AuftragTableID == null)
                        {
                            strError = strError + "LAusgangTableID: => NULL " + Environment.NewLine;
                        }
                        else
                        {
                            strError = strError + "LAusgangTableID: " + this.LAusgangTableID.ToString() + " | Length: " + this.LAusgangTableID.ToString().Length.ToString() + Environment.NewLine;
                        }
                        if (this.Path == null)
                        {
                            strError = strError + "Pfad:  => NULL " + Environment.NewLine;
                        }
                        else
                        {
                            strError = strError + "Pfad: " + this.Path + " | Length: " + this.Path.Length.ToString() + Environment.NewLine;
                        }
                        if (this.Filename == null)
                        {
                            strError = strError + "ScanFileName:  => NULL " + Environment.NewLine;
                        }
                        else
                        {
                            strError = strError + "ScanFileName: " + this.Filename + " | Length: " + this.Filename.Length.ToString() + Environment.NewLine;
                        }
                        if (this.Picnum == null)
                        {
                            strError = strError + "Picnum:  => NULL " + Environment.NewLine;
                        }
                        else
                        {
                            strError = strError + "PicNum: " + this.Picnum.ToString() + " | Length: " + this.Picnum.ToString().Length.ToString() + Environment.NewLine;
                        }
                        if (this.ImageArt == null)
                        {
                            strError = strError + "ImageArt:  => NULL " + Environment.NewLine;
                        }
                        else
                        {
                            strError = strError + "ImageArt: " + this.ImageArt.ToString() + " | Length: " + this.ImageArt.Length.ToString() + Environment.NewLine;
                        }
                        if (this.AuftragPosTableID == null)
                        {
                            strError = strError + "AuftragPosTableID:  => NULL " + Environment.NewLine;
                        }
                        else
                        {
                            strError = strError + "AuftragPosTableID: " + this.AuftragPosTableID.ToString() + " | Length: " + this.AuftragPosTableID.ToString().Length.ToString() + Environment.NewLine;
                        }
                        if (strParameterImage == null)
                        {
                            strError = strError + "DocImage:  => NULL " + Environment.NewLine;
                        }
                        else
                        {
                            strError = strError + "DocImage: " + strParameterImage + " | Length: " + strParameterImage.Length.ToString() + Environment.NewLine;
                        }
                        if (this.TableName == null)
                        {
                            strError = strError + "TableName:  => NULL " + Environment.NewLine;
                        }
                        else
                        {
                            strError = strError + "TableName: " + this.TableName.ToString() + " | Length: " + this.TableName.Length.ToString() + Environment.NewLine;
                        }
                        if (this.TableID == null)
                        {
                            strError = strError + "TableID:  => NULL " + Environment.NewLine;
                        }
                        else
                        {
                            strError = strError + "TableID: " + this.TableID.ToString() + " | Length: " + this.TableID.ToString().Length.ToString() + Environment.NewLine;
                        }
                        if (strParameterThumb == null)
                        {
                            strError = strError + "Thumbnail:  => NULL " + Environment.NewLine;
                        }
                        else
                        {
                            strError = strError + "Thumbnail: " + strParameterThumb + " | Length: " + strParameterThumb.Length.ToString() + Environment.NewLine;
                        }

                        strError = strError + "SQL-Statement:" + Environment.NewLine +
                                   strSQL + Environment.NewLine;

                        strError = strError + Environment.NewLine + ex.ToString();
                        EMail.Message = strError;
                        EMail.SendError();
                    }
                    finally
                    {

                    }

                }

            }
            catch (Exception ex)
            {

            }
        }

        ///<summary>clsAuftragScan / CreateImageParameter</summary>
        ///<remarks></remarks>
        ///<return></return>
        private SqlParameter CreateImageParameter(string myPName, Image myImageToDB)
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
        ///<summary>clsAuftragScan / GetMaxPicNumByAuftrag</summary>
        ///<remarks>Ermittel anhand der AuftragsID die Anzahl der bereits gespeicherten 
        ///         Dokumente (PicNum).</remarks>
        ///<return>myIMaxPicNum</return>
        private Int32 GetMaxPicNum(string myTableName, decimal myTableID)
        {
            Int32 myIMaxPicNum = 0;

            string strSql = string.Empty;
            strSql = "Select MAX(PicNum) FROM DocScan WHERE TableName='" + myTableName + "' AND TableID=" + (Int32)myTableID + ";";

            string myStrReturn = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            if (myStrReturn != String.Empty)
            {
                if (!Int32.TryParse(myStrReturn, out myIMaxPicNum))
                {
                    myIMaxPicNum = 0;
                }
            }
            else
            {
                myIMaxPicNum = 0;
            }
            return myIMaxPicNum + 1;
        }
        ///<summary>clsAuftragScan / GetMaxPicNumByAuftrag</summary>
        ///<remarks>Ermittel anhand der AuftragsID die Anzahl der bereits gespeicherten 
        ///         Dokumente (PicNum).</remarks>
        ///<return>myIMaxPicNum</return>
        public bool UpdateIsForSPLMessage(bool mySelect)
        {
            string strSql = string.Empty;
            strSql = "Update DocScan SET IsForSPLMessage=" + Convert.ToInt32(mySelect) +
                                " WHERE ID =" + (Int32)this.ID + ";";

            return clsSQLcon.ExecuteSQL(strSql, BenutzerID);
        }
        /*********************************************************************************************
         *                          static 
         * ******************************************************************************************/
        ///<summary>clsAuftragScan / GetImages</summary>
        ///<remarks></remarks>
        ///<return>DataTable</return>
        public static DataTable GetImages(Globals._GL_USER myGLUser, string myTableName, decimal myTableID)
        {
            string strSQL = "Select " +
                                    "* " +
                                    "FROM DocScan " +
                                    "WHERE TableName='" + myTableName + "'" +
                                    " AND TableID=" + (Int32)myTableID + " ; ";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "DocsImage");

            return dt;
        }
        ///<summary>clsAuftragScan / GetMaxPicNumByAuftrag</summary>
        ///<remarks>Ermittel anhand der AuftragsID die Anzahl der bereits gespeicherten 
        ///         Dokumente (PicNum).</remarks>
        ///<return>myIMaxPicNum</return>
        public Int32 GetMaxPicNumByAuftrag()
        {
            Int32 myIMaxPicNum = 0;

            string strSql = string.Empty;
            //unterscheiden nach dem Vorang
            if (m_dec_AuftragID > 0)
            {
                strSql = "Select MAX(PicNum) FROM DocScan WHERE AuftragID='" + m_dec_AuftragID + "'";
            }
            else if (m_dec_LEingangID > 0)
            {
                strSql = "Select MAX(PicNum) FROM DocScan WHERE LEingangID='" + m_dec_LEingangID + "'";
            }
            else if (m_dec_LAusgangID > 0)
            {
                strSql = "Select MAX(PicNum) FROM DocScan WHERE LAusgangID='" + m_dec_LAusgangID + "'";
            }
            //CHeck SQL leer
            if (strSql != string.Empty)
            {
                string myStrReturn = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                if (myStrReturn != String.Empty)
                {
                    if (!Int32.TryParse(myStrReturn, out myIMaxPicNum))
                    {
                        myIMaxPicNum = 1;
                    }
                }
                else
                {
                    myIMaxPicNum = 0;
                }
            }
            return myIMaxPicNum;
        }
        ///<summary>clsAuftragScan / AddDocScanDataDB</summary>
        ///<remarks>Ermittel anhand der AuftragsID die Anzahl der bereits gespeicherten 
        ///         Dokumente (PicNum).</remarks>
        ///<return>myIMaxPicNum</return>
        public void AddDocScan()
        {
            string strSQL = "INSERT INTO DocScan " +
                                             "(AuftragTableID, LEingangTableID, LAusgangTableID, Pfad, ScanFilename, PicNum, ImageArt, AuftragPosTableID, TableName, TableID) " +
                                             "VALUES ('" + AuftragTableID + "', '" +
                                                           LEingangTableID + "', '" +
                                                           LAusgangTableID + "', '" +
                                                           m_str_Path + "', '" +
                                                           m_str_Filename + "', '" +
                                                           m_i_picnum + "', '" +
                                                           m_str_ImageArt + "', '" +
                                                           m_dec_AuftragPosTableID + "' " +
                                                           ", '" + this.TableName + "' " +
                                                           ", " + (Int32)this.TableID +

                                                           ")";

            strSQL = strSQL +
                    " Update DocScan Set ScanFilename =CAST((SELECT IDENT_CURRENT('DocScan')) as varchar(255))+'.jpg' " +
                                                            "WHERE ID=(SELECT IDENT_CURRENT('DocScan')) ";


            //Eintarg in DB
            if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "DocScan", BenutzerID))
            {
                //ID für den DB Eintrag wird ermittelt
                strSQL = string.Empty;
                strSQL = "SELECT IDENT_CURRENT('DocScan')";
                string myStrDocScanID = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                if (myStrDocScanID != string.Empty)
                {
                    m_dec_DocScanID = Convert.ToDecimal(myStrDocScanID);
                }

                //Add Logbucheintrag Exception
                string myBeschreibung = "Dokument hinterlegt: DocSan ID:" + myStrDocScanID;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);

                //Wenn neue Dokumente hinzugefügt werden, dann müssen die Datensätze der Table AuftragRead 
                //mit der entsprechenden AuftragTable ID gelöscht werden
                clsAuftragRead read = new clsAuftragRead();
                read._GL_User = this._GL_User;
                read.DeleteReadAuftragAuftragTableID(m_dec_AuftragID);
            }
        }
        ///<summary>clsAuftragScan / SetDocScanClassDatenByDocScanTableID</summary>
        ///<remarks>Läd die DocScanDaten und setzt die entsprechenden Daten der Klasse</remarks>
        public void SetDocScanClassDatenByDocScanTableID()
        {
            string strSQL = string.Empty;
            DataTable dt = new DataTable();

            if (m_dec_DocScanID > 0)
            {
                strSQL = "SELECT * FROM DocScan WHERE ID='" + m_dec_DocScanID + "'";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "DocScan");

                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    m_dec_DocScanID = (decimal)dt.Rows[i]["ID"];
                    AuftragTableID = (decimal)dt.Rows[i]["AuftragTableID"];
                    LEingangTableID = (decimal)dt.Rows[i]["LEingangTableID"];
                    LAusgangTableID = (decimal)dt.Rows[i]["LAusgangTableID"];
                    m_i_picnum = (Int32)dt.Rows[i]["PicNum"];
                    m_str_Path = dt.Rows[i]["Pfad"].ToString();
                    m_str_Filename = dt.Rows[i]["ScanFilename"].ToString();
                    m_str_ImageArt = dt.Rows[i]["ImageArt"].ToString();
                    m_dec_AuftragPosTableID = (decimal)dt.Rows[i]["AuftragPosTableID"];
                    AuftragImageOut = (Image)dt.Rows[i]["DocImage"];
                }
            }
        }
        ///<summary>clsAuftragScan / GetDocScanImageTable</summary>
        ///<remarks>Läd alle Scan-Datensätze zu einer AuftragsID. Anschließend wird die Datatable um 
        ///         eine Spalte "AuftragImage" erweitert und die entsprechenden Docs als Byte[] in 
        ///         diese Spalte geladen.</remarks>
        ///<return>DocScan-Table</return>
        public DataTable GetDocScanImageTable(decimal myAuftragID)
        {
            //Baustelle
            string strSQL = string.Empty;
            if (myAuftragID > 0)
            {
                strSQL = "SELECT a.* " +
                                ", b.ANr AS AuftragNr " +
                                                            "FROM DocScan a " +
                                                            "INNER JOIN Auftrag b ON b.ID = a.AuftragTableID " +
                                                            "WHERE b.ID=" + myAuftragID + " ;";
            }
            DataTable dt = new DataTable();
            if (strSQL != string.Empty)
            {
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, this._GL_User.User_ID, "DocSan");
                //Column für Image hinzufügen
                //DataColumn col = new DataColumn("AuftragImage");
                //col.DataType = System.Type.GetType("System.Byte[]");
                //dt.Columns.Add(col);

                //Über die Pfadangabe die Datei von der HDD laden
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    //Image Pfad ermitteln
                    string myStrImagePfad = string.Empty;
                    if (dt.Rows[i]["Pfad"].ToString() != string.Empty)
                    {
                        myStrImagePfad = dt.Rows[i]["Pfad"].ToString();
                        if (dt.Rows[i]["ScanFilename"].ToString() != string.Empty)
                        {
                            myStrImagePfad = myStrImagePfad + "\\" + dt.Rows[i]["ScanFilename"].ToString();
                        }
                    }
                    //Pfadprüfen 
                    if (File.Exists(myStrImagePfad))
                    {
                        //Bitmap bm = new Bitmap(myStrImagePfad);
                        //MemoryStream ms = new MemoryStream();
                        //bm.Save(ms, ImageFormat.Jpeg);
                        //byte[] imgbyte = ms.ToArray();
                        ////Doc wird in den Table geladen
                        //dt.Rows[i]["DocImage"] = imgbyte;
                        ////dt.Rows[i]["DocImage"] = byteArrayToImage(imgbyte);

                    }

                }
            }
            return dt;
        }
        /////<summary>clsAuftragScan / GetDocScanImageTable</summary>
        /////<remarks>Läd alle Scan-Datensätze zu einer AuftragsID. Anschließend wird die Datatable um 
        /////         eine Spalte "AuftragImage" erweitert und die entsprechenden Docs als Byte[] in 
        /////         diese Spalte geladen.</remarks>
        /////<return>DocScan-Table</return>
        //public static DataTable GetDocScanImageTable(Globals._GL_USER GL_User, decimal myDecAuftragID, decimal myDecLEingangID, decimal myDecLAusgangID)
        //{

        //    //Baustelle
        //    string strSQL = string.Empty;

        //    if (myDecAuftragID > 0)
        //    {
        //        strSQL = "SELECT a.* " +
        //                        ", b.ANr AS AuftragNr " +
        //                                                    "FROM DocScan a " +
        //                                                    "INNER JOIN Auftrag b ON b.ID = a.AuftragID " +
        //                                                    "WHERE a.AuftragID='" + myDecAuftragID + "';";
        //    }
        //    else if (myDecLEingangID > 0)
        //    {
        //        strSQL = "Select * From DocScan WHERE LEingangID='" + myDecLEingangID + "'";
        //    }
        //    else if (myDecLAusgangID > 0)
        //    {
        //        strSQL = "Select * From DocScan WHERE LAusgangID='" + myDecLAusgangID + "'";
        //    }

        //    DataTable dt = new DataTable();
        //    if (strSQL != string.Empty)
        //    {
        //        dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, GL_User.User_ID, "DocSan");
        //        //Column für Image hinzufügen
        //        //DataColumn col = new DataColumn("AuftragImage");
        //        //col.DataType = System.Type.GetType("System.Byte[]");
        //        //dt.Columns.Add(col);

        //        //Über die Pfadangabe die Datei von der HDD laden
        //        for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
        //        {
        //            //Image Pfad ermitteln
        //            string myStrImagePfad = string.Empty;
        //            if (dt.Rows[i]["Pfad"].ToString() != string.Empty)
        //            {
        //                myStrImagePfad = dt.Rows[i]["Pfad"].ToString();
        //                if (dt.Rows[i]["ScanFilename"].ToString() != string.Empty)
        //                {
        //                    myStrImagePfad = Application.StartupPath + myStrImagePfad + dt.Rows[i]["ScanFilename"].ToString();
        //                }
        //            }
        //            //Pfadprüfen 
        //            if (File.Exists(myStrImagePfad))
        //            {
        //                Bitmap bm = new Bitmap(myStrImagePfad);
        //                MemoryStream ms = new MemoryStream();
        //                bm.Save(ms, ImageFormat.Jpeg);
        //                byte[] imgbyte = ms.ToArray();
        //                //Doc wird in den Table geladen
        //                dt.Rows[i]["DocImage"] = imgbyte;
        //            }

        //        }
        //    }
        //    return dt;
        //}
        ///<summary>clsAuftragScan / SaveScanDocToHDD</summary>
        ///<remarks>Speichert das Dokument unter dem angegebenen Filenamen auf der HDD ab.</remarks>
        public void UpdateRotateDocImage()
        {
            if (AuftragImageIn != null)
            {
                AuftragImageIn.Save(StartupPath + m_str_SavePathAndFilename);
                //AuftragImageIn.Save(Application.StartupPath + m_str_SavePathAndFilename);
                UpdateAuftragImage();
            }
        }
        ///<summary>clsAuftragScan / SaveScanDocToHDD</summary>
        ///<remarks>Speichert das Dokument unter dem angegebenen Filenamen auf der HDD ab.</remarks>
        public void SaveScanDocToHDD()
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
            AuftragImageIn.Save(m_str_SavePathAndFilenameTMP, jpegCodec, encoderParams);

            //to PDF
            //var reportProzessor = new Telerik.Reporting.Processing.ReportProcessor();
            //var typeReportSource = new Telerik.Reporting.TypeReportSource();
            //typeReportSource.TypeName = "TestReport";
            //var result = reportProzessor.RenderReport("PDF", typeReportSource, null); 


            //AuftragImageIn.Save(m_str_SavePathAndFilename);
            Int32 iWidth = 795;
            Int32 iHeight = 1095;
            using (var absentRectangleImage = Bitmap.FromFile(m_str_SavePathAndFilenameTMP))
            {
                using (var currentTile = new Bitmap(iWidth, iHeight))
                {
                    currentTile.SetResolution(absentRectangleImage.HorizontalResolution, absentRectangleImage.VerticalResolution);
                    //currentTile.SetResolution(iHeight, iWidth);
                    using (var currentTileGraphics = Graphics.FromImage(currentTile))
                    {
                        currentTileGraphics.Clear(Color.Transparent);
                        var absentRectangleArea = new Rectangle(0, 0, iWidth, iHeight);
                        // currentTileGraphics.DrawImage( absentRectangleImage, 0, 0, absentRectangleArea, GraphicsUnit.Pixel );
                        currentTileGraphics.DrawImage(absentRectangleImage, 0, 0, absentRectangleArea, GraphicsUnit.Pixel);

                    }
                    currentTile.Save(m_str_SavePathAndFilename);
                    AuftragImageIn = (Image)currentTile;
                    //Save in DB
                    //UpdateAuftragImage();
                }
            }
            System.IO.File.Delete(m_str_SavePathAndFilenameTMP);
        }
        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
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
        ///<summary>clsAuftragScan / SaveScanDocToHDD</summary>
        ///<remarks>Speichert das Dokument unter dem angegebenen Filenamen auf der HDD ab.</remarks>
        private void UpdateAuftragImage()
        {
            if (m_dec_DocScanID > 0)
            {
                //AuftragImageIn.Save("D:\\gespeicher_JPG.jpg", ImageFormat.Jpeg);
                if (AuftragImageIn != null)
                {
                    MemoryStream mem = new MemoryStream();

                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(mem, AuftragImageIn);
                    mem.Seek(0, 0);
                    AuftragImageIn.Save(mem, ImageFormat.Jpeg);
                    Int32 intLength = Convert.ToInt32(mem.Length.ToString());
                    mem.Seek(0, 0);
                    byte[] byteArrayIn = new byte[intLength];
                    mem.Read(byteArrayIn, 0, intLength); ;
                    mem.Close();

                    string strSQL = "Update DocScan SET DocImage=@p WHERE ID='" + m_dec_DocScanID + "'";
                    InsertImage(strSQL, byteArrayIn, "@p");
                    AuftragImageIn.Dispose();
                }
            }
        }
        ///<summary>clsAuftragScan / SaveScanDocToHDD</summary>
        ///<remarks>Speichert das Dokument unter dem angegebenen Filenamen auf der HDD ab.</remarks>
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
        ///<summary>clsAuftragScan / SaveScanDocToHDD</summary>
        ///<remarks>Speichert das Dokument unter dem angegebenen Filenamen auf der HDD ab.</remarks>
        public void DeleteByAuftrag(decimal myAuftragTableID)
        {
            string strSQL = clsDocScan.GetSQLDeleteByAuftrag(myAuftragTableID);
            clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "ScanDocDelete", this.BenutzerID);
        }
        ///<summary>clsAuftragScan / Delete</summary>
        ///<remarks>Image per ID löschen</remarks>
        public void Delete()
        {
            if (this.ID > 0)
            {
                try
                {
                    string strFilename = this.Filename;
                    string strSQL = string.Empty;
                    strSQL = "DELETE FROM DocScan WHERE ID=" + (Int32)this.ID + ";";
                    bool bOK = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
                    if (bOK)
                    {
                        //Add Logbucheintrag Eintrag
                        string Beschreibung = "Dokument: ID " + this.ID + "/ [Table/ID]: " + this.TableName + "/" + this.TableID + " wurde gelöscht!";
                        Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

                        switch (this.TableName)
                        {
                            case "Artikel":
                                clsArtikelVita.DeleteImageFromArtikel(this._GL_User, this.TableID, enumLagerAktionen.ImageToArtikel.ToString(), strFilename);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }



        /************************************************************************************
         *                      public static
         * *********************************************************************************/
        ///<summary>clsAuftragScan / GetSQLDeleteByAuftrag</summary>
        ///<remarks>SQL-Anweisung zum Löschen der Datensätze über die einen Auftrag</remarks>
        public static string GetSQLDeleteByAuftrag(decimal myAuftragTableID)
        {
            string strSQL = "DELETE FROM DocScan WHERE AuftragTableID=" + myAuftragTableID;
            return strSQL;
        }
        ///<summary>clsAuftragScan / GetDocScanIDList</summary>
        ///<remarks>Ermittelt eine Liste mit den ID der hinterlegten Dokumente zu einem Auftrag. Die 
        ///         Dokumente sind mit der ID als Filename</remarks>
        public static DataTable GetDocScanTableByAuftrag(decimal myAuftragTableID, Globals._GL_USER myGLUser)
        {
            string strSQL = string.Empty;
            strSQL = "Select * FROM DocScan WHERE AuftragTableID=" + myAuftragTableID + ";";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "DocScan");
            return dt;
        }

        //
        //
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            ms.Seek(0, 0);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        //
        //
        //
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        //
        public void StartScan()
        {
            OpenScanFrm();
        }
        //
        private void OpenScanFrm()
        {

        }
        //
        //
        //
        public static DataTable GetAuftragImageTable(decimal AuftragID)
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;

            Command.CommandText = "Select " +
                                         "ID, " +
                                         "AuftragID, " +
                                         "AuftragImage, " +
                                         "ScanFilename, " +
                                         "PicNum, " +
                                         "ImageArt, " +
                                         "AuftragPosTableID " +
                                         "FROM DocScan " +
                                         "WHERE AuftragID='" + AuftragID + "'";

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
        ////
        ////-------- Read one Image ------------------
        ////
        //public byte[] ReadImage()
        //{
        //    byte[] ImageByte;
        //    SqlDataAdapter ada = new SqlDataAdapter();
        //    SqlCommand Command = new SqlCommand();
        //    Command.Connection = Globals.SQLcon.Connection;
        //    ada.SelectCommand = Command;

        //    Command.CommandText = "Select AuftragImage FROM DocScan " +
        //                                                  "WHERE " +
        //                                                  "AuftragID='" + m_dec_AuftragID + "' AND " +
        //                                                  "PicNum='" + m_i_picnum + "' AND " +
        //                                                  "ImageArt='" + m_str_ImageArt + "'";

        //    Globals.SQLcon.Open();
        //    object obj = Command.ExecuteScalar();

        //    if ((obj == null) | (obj is DBNull))
        //    {
        //        ImageByte = null;
        //    }
        //    else
        //    {
        //        ImageByte = (byte[])obj;
        //    }
        //    Command.Dispose();
        //    Globals.SQLcon.Close();
        //    if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
        //    {
        //        Command.Connection.Close();
        //    }

        //    return ImageByte;
        //}
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
            Image img = (Image)bmp;
            return img;
        }
        /****
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

                string strSQL = "Update DocScan Set AuftragImage =@p WHERE ID ='" + m_dec_DocScanID + "'";
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
         * ***/
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
                InsertCommand.CommandText = "INSERT INTO DocScan " +
                                                         "(AuftragID, Pfad, ScanFilename, PicNum) " +
                                                         "VALUES (" + m_dec_AuftragID + ", '" + m_str_Path + "', '" + m_str_Filename + "', " + m_i_picnum + ")";

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
            Command.CommandText = "Select ID FROM DocScan WHERE AuftragID='" + m_dec_AuftragID + "' AND PicNum='" + m_i_picnum + "'";
            Globals.SQLcon.Open();
            object result = Command.ExecuteScalar();
            if (result == null)
            {
                IsIn = false;
            }
            else
            {
                decimal ID = (decimal)Command.ExecuteScalar();
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
            string strSQL = string.Empty;
            strSQL = "Select a.ID FROM DocScan a INNER JOIN Auftrag b ON a.AuftragID = b.ID " +
                                                                                "WHERE b.ID='" + m_dec_AuftragID + "'";
            IsIn = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
            return IsIn;
        }
        //
        //-------------- Check ob Abholschein vorhanden ist --------------------
        //
        public static bool CheckDocScanIsIn(Globals._GL_USER myGL_User, decimal myDecAuftragNr, string strBeschreibung)
        {
            bool IsIn = false;

            //Anhand der Auftragsnummer muss erst 
            decimal myDecAuftragTableID = clsAuftrag.GetAuftragIDbyANr(myGL_User, myDecAuftragNr);
            if (myDecAuftragTableID > 0)
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select Top(1)ID FROM DocScan WHERE AuftragID='" + myDecAuftragTableID + "' AND " +
                                                                    "ImageArt='" + strBeschreibung + "'";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGL_User.User_ID, "dtTmp");

                if (dt.Rows.Count == 0)
                {
                    IsIn = false;
                }
                else
                {
                    IsIn = true;
                }
            }
            return IsIn;
        }

        //
        //---------------- Image  / Bilder löschen ---------------------
        //
        public static void DeleteAuftragScan(decimal AuftragID, string ImageArt)
        {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "DELETE FROM DocScan WHERE AuftragID='" + AuftragID + "' AND ImageArt='" + ImageArt + "'";
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
        public void DeleteAuftragScanByDocScanID()
        {
            if (m_dec_DocScanID > 0)
            {
                try
                {
                    string strSQL = string.Empty;
                    strSQL = "DELETE FROM DocScan WHERE ID='" + m_dec_DocScanID + "'";
                    clsSQLcon.ExecuteSQL(strSQL, BenutzerID);

                }
                catch (Exception ex)
                {
                    //System.Windows.Forms.MessageBox.Show(ex.ToString());
                    //Add Logbucheintrag Exception
                    string Beschreibung = "Exception: " + ex;
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), Beschreibung);
                }
                finally
                {
                    //Add Logbucheintrag Eintrag
                    string Beschreibung = "Dokument: Auftrag " + m_dec_AuftragID + "/" + m_dec_AuftragPos + " - " + m_str_ImageArt + "wurde gelöscht!";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
                }
            }
        }
        //
        //
        public void DeleteAuftragScanByAuftragPosTableIDAndImageArt()
        {
            if ((m_dec_AuftragPosTableID > 0) && (m_str_ImageArt != string.Empty))
            {
                try
                {
                    string strSQL = string.Empty;
                    strSQL = "DELETE FROM DocScan WHERE " +
                                                           "AuftragPosTableID='" + m_dec_AuftragPosTableID + "' " +
                                                           "AND ImageArt='" + m_str_ImageArt + "' ";
                    clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
                }
                catch (Exception ex)
                {
                    //System.Windows.Forms.MessageBox.Show(ex.ToString());
                    //Add Logbucheintrag Exception
                    string Beschreibung = "Exception: " + ex;
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), Beschreibung);
                }
                finally
                {
                    //Add Logbucheintrag Eintrag
                    string Beschreibung = "Dokument: Auftrag " + m_dec_AuftragID + "/" + m_dec_AuftragPos + " - " + m_str_ImageArt + "wurde gelöscht!";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
                }
            }
        }
        //

        //
        public static decimal GetAuftragPosTableIDByDocScanTableID(Globals._GL_USER myGLUser, decimal myDecDocScanTablID)
        {
            string strSQL = string.Empty;
            strSQL = "SELECT AuftragPosTableID FROM DocScan WHERE ID='" + myDecDocScanTablID + "' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        //public static List<DocScanView>GetSearchValueList(Globals._GL_USER myGLUser, int myWorkspaceId, int SearchValue, string mySearchField)
        //{
        //    ArticleViewData art = new ArticleViewData();
        //    EingangViewData ein = new EingangViewData();
        //    AusgangViewData aus = new AusgangViewData();

        //    List < DocScanView > retList = new List < DocScanView >();
        //    string strSQL = string.Empty;
        //    strSQL = "SELECT * FROM DocScan WHERE "; 
        //    switch (mySearchField)
        //    {
        //        case ArchiveViewData.const_Datafield_LvsID:
        //            art = new ArticleViewData(SearchValue, (int)myGLUser.User_ID, myWorkspaceId);
        //            ein = new EingangViewData(art.Artikel.LEingangTableID, (int)myGLUser.User_ID, false);

        //            strSQL += "(TableName = '" + enumDatabaseTableNames.Artikel + "' and TableID=" + art.Artikel.Id + ") ";
        //            strSQL += " OR ";
        //            strSQL += "(TableName = '" + enumDatabaseTableNames.LEingang + "' and TableID=" + art.Artikel.LEingangTableID + ") ";

        //            if (art.Artikel.LAusgangTableID > 0)
        //            {
        //                aus = new AusgangViewData(art.Artikel.LAusgangTableID, (int)myGLUser.User_ID, false);
        //                strSQL += " OR ";
        //                strSQL += "(TableName = '" + enumDatabaseTableNames.LAusgang + "' and TableID=" + art.Artikel.LAusgangTableID + ") ";
        //            }
        //            break;
        //        case ArchiveViewData.const_Datafield_EingangID:
        //            ein = new EingangViewData(SearchValue, (int)myGLUser.User_ID, myWorkspaceId);
        //            strSQL += "(TableName = '" + enumDatabaseTableNames.LEingang + "' and TableID=" + ein.Eingang.Id + ") ";

        //            break;
        //        case ArchiveViewData.const_Datafield_AusgangID:
        //            aus = new AusgangViewData(SearchValue, (int)myGLUser.User_ID, myWorkspaceId);
        //            strSQL += "(TableName = '" + enumDatabaseTableNames.LAusgang + "' and TableID=" + aus.Ausgang.Id + ") ";
        //            break;
        //        default:
        //            strSQL = string.Empty;
        //            break;
        //    }
        //    DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "DocScan", "DocScan", myGLUser.User_ID);
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        int iTmp = 0;
        //        int.TryParse(dr["ID"].ToString(), out iTmp);
        //        if (iTmp > 0)
        //        {
        //            ImageViewData dsViewData = new ImageViewData(myGLUser, iTmp);
        //            DocScanView tmpView = new DocScanView();
        //            tmpView.DocScan = dsViewData.DocScan.Copy();
        //            switch (mySearchField)
        //            {
        //                case ArchiveViewData.const_Datafield_LvsID:
        //                    tmpView.LVSNr = art.Artikel.LVS_ID;
        //                    tmpView.LEingangID = ein.Eingang.LEingangID;
        //                    if (art.Artikel.LAusgangTableID > 0)
        //                    {
        //                        tmpView.LAusgangID = aus.Ausgang.LAusgangID;
        //                    }
        //                    break;
        //                case ArchiveViewData.const_Datafield_EingangID:
        //                    tmpView.LEingangID = ein.Eingang.LEingangID;
        //                    break;
        //                case ArchiveViewData.const_Datafield_AusgangID:
        //                    tmpView.LAusgangID = aus.Ausgang.LAusgangID;
        //                    break;
        //            }
        //            retList.Add(tmpView);
        //        }
        //    }
        //    return retList;
        //}


    }
}

