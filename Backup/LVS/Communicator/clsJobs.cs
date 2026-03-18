using LVS.Constants;
using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsJobs
    {
        public clsJobs Copy()
        {
            return (clsJobs)this.MemberwiseClone();
        }

        public const string const_PostBy_Odette = "ODETTE";
        public const string const_PostBy_FTP = "FTP";
        public const string const_PostBy_SFTP = "SFTP";

        public clsSQLCOM SQLConIntern = new clsSQLCOM();
        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GL_User;
        public clsftp FTP = new clsftp();
        public clsSFTP SFTP = new clsSFTP();
        public clsASNTyp AsnTyp;
        public clsTP FtpOrSftp { get; set; }

        public clsADR ADR;

        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        private string _PathFile;
        private string _PathDirectory;
        private DataTable _dtJobsToDoIN;
        private DataTable _dtJobsToDoOUT;
        private DataTable _dtJobsAbrufe;
        private DataTable _dtJobs;


        public decimal ID { get; set; }
        private decimal _AdrVerweisID;
        public decimal AdrVerweisID
        {
            get
            {
                return _AdrVerweisID;
            }
            set
            {
                _AdrVerweisID = value;
                if (_AdrVerweisID > 0)
                {
                    this.ADR = new clsADR();
                    this.ADR.InitClass(this.GL_User, this.GLSystem, _AdrVerweisID, true);
                }

            }
        }
        public string ASNFileTyp { get; set; }
        public decimal ASNTypID { get; set; }
        public string Direction { get; set; }
        public bool activ { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public decimal ASNArtID { get; set; }
        public string PathFile
        {
            get
            {
                _PathFile = this.GLSystem.VE_OdettePath + Path + FileName;
                return _PathFile;
            }
            set { _PathFile = value; }
        }
        public string PathDirectory
        {
            get
            {
                _PathDirectory = this.GLSystem.VE_OdettePath + Path;
                return _PathDirectory;
            }
            set { _PathDirectory = value; }
        }
        public DataTable dtJobsToDoIN
        {
            get
            {
                _dtJobsToDoIN = this.GetJobs(true, enumJobDirection.IN.ToString(), false, false);
                return _dtJobsToDoIN;
            }
            set { _dtJobsToDoIN = value; }
        }
        public DataTable dtJobsToDoOUT
        {
            get
            {
                _dtJobsToDoOUT = this.GetJobs(true, enumJobDirection.OUT.ToString(), false, false);
                return _dtJobsToDoOUT;
            }
            set { _dtJobsToDoOUT = value; }
        }
        public DataTable dtJobsAbrufe
        {
            get
            {
                _dtJobsAbrufe = this.GetJobs(true, enumJobDirection.IN.ToString(), true, false);
                return _dtJobsAbrufe;
            }
            set { _dtJobsAbrufe = value; }
        }
        public DataTable dtJobs
        {
            get
            {
                _dtJobs = this.GetJobs(false, null, false, true);
                return _dtJobs;
            }
            set { _dtJobs = value; }
        }
        public DataTable dtJobsByAdrVerweis
        {
            get
            {
                DataTable dtReturn = this.GetJobs(false, null, false, true);
                dtReturn.DefaultView.RowFilter = "AdrVerweisId=" + this.AdrVerweisID;
                dtReturn.DefaultView.Sort = "ArbeitsbereichsID";
                return dtReturn.DefaultView.ToTable();
            }
            set { _dtJobs = value; }
        }
        public string PostBy { get; set; }
        public string FTPServer { get; set; }
        public string FTPUser { get; set; }
        public string FTPPass { get; set; }
        public string FTPFileName { get; set; }
        public bool FTPUsePassiveMode { get; set; }
        public string SearchFileName { get; set; }
        public decimal MandantenID { get; set; }
        public decimal ArbeitsbereichID { get; set; }
        public string EinheitLM { get; set; }
        public DateTime ValidFrom { get; set; }
        public string VerweisVDA4905 { get; set; }
        public bool UseCRLF { get; set; }
        public bool CreateOdetteStart { get; set; }
        public string OdetteStartFileName
        {
            get
            {
                string strTmp = "~start.job";
                return strTmp;
            }
        }

        public string CheckTransferPath { get; set; }
        public string ASNFileStorePath { get; set; }
        public string ASNFileStorePathDirectory
        {
            get
            {
                string strReturn = this.GLSystem.VE_OdettePath + ASNFileStorePath;
                return strReturn;
            }
        }
        public string ErrorPath { get; set; }
        public string TransferFileName { get; set; }
        public DateTime ActionDate { get; set; }

        private string _Periode;
        public string Periode
        {
            get
            {
                return _Periode;
            }
            set
            {
                _Periode = value;
                if (_Periode.Equals(string.Empty))
                {
                    _Periode = enumPeriode.NotSet.ToString();
                }
            }
        }
        public string ViewProzessName { get; set; }


        public string CheckCloneFilePath { get; set; }
        public string CheckCloneFileName { get; set; }
        public bool CheckCloneFile { get; set; }
        public string DelforVerweis { get; set; }

        /******************************************************************************
         *                      Methoden / Procedure
         * ***************************************************************************/
        ///<summary>clsJobs / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_SYSTEM myGLSystem, Globals._GL_USER myGLUser, bool myIsWatchDogMod)
        {
            this.GL_User = myGLUser;
            this.GLSystem = myGLSystem;

            //if (!myIsWatchDogMod)
            //{
            //    FillByASNFileTypAndASNTyp(clsASNArt.const_Art_WatchDog, 0, clsASNArt.GetWatchDogID(), 0, myIsWatchDogMod);
            //}
            FillByASNFileTypAndASNTyp(constValue_AsnArt.const_Art_WatchDog, 0, clsASNArt.GetWatchDogID(), 0, myIsWatchDogMod);

            FTP = new clsftp();
            FTP.InitClass(this, null);
        }
        ///<summary>clsJobs / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSql = "INSERT INTO Jobs (AdrVerweisID, ASNFileTyp, ASNTypID, Direction, activ, Path, Filename, ASNArtID" +
                                               ", PostBy, FTPServer, FTPUser, FTPPass, FTPFileName, SearchFileName" +
                                               ", MandantenID, ArbeitsbereichsID, EinheitLM, validFrom, VerweisVDA4905, UseCRLF" +
                                               ", CreateOdetteStart, CheckTransferPath, ASNFileStorePath , ErrorPath, TransferFileName" +
                                               ", ActionDate, Periode, ViewProzessName, CheckCloneFilePath, CheckCloneFileName,CheckCloneFile " +
                                               ", FTPUsePassiveMode, DelforVerweis" +
                                              ") " +
                                  "VALUES (" + AdrVerweisID +
                                         ", '" + ASNFileTyp + "'" +
                                         "," + ASNTypID +
                                         ",'" + Direction + "'" +
                                         "," + Convert.ToInt32(activ) +
                                         ",'" + Path + "'" +
                                         ",'" + FileName + "' " +
                                         "," + ASNArtID +
                                         ",'" + PostBy + "' " +
                                         ",'" + FTPServer + "' " +
                                         ",'" + FTPUser + "' " +
                                         ",'" + FTPPass + "' " +
                                         ",'" + FTPFileName + "' " +
                                         ",'" + SearchFileName + "' " +
                                         ", " + MandantenID +
                                         ", " + ArbeitsbereichID +
                                         ", '" + EinheitLM + "'" +
                                         ", '" + Globals.DefaultDateTimeMinValue + "'" +
                                         ", '" + VerweisVDA4905 + "'" +
                                         ", " + Convert.ToInt32(this.UseCRLF) +
                                         ", " + Convert.ToInt32(this.CreateOdetteStart) +
                                         ", '" + this.CheckTransferPath + "'" +
                                         ", '" + this.ASNFileStorePath + "'" +
                                         ", '" + this.ErrorPath + "'" +
                                         ", '" + this.TransferFileName + "'" +
                                         ", '" + this.ActionDate + "'" +
                                         ", '" + this.Periode + "'" +
                                         ", '" + this.ViewProzessName + "'" +
                                         ", '" + this.CheckCloneFilePath + "'" +
                                         ", '" + this.CheckCloneFileName + "'" +
                                         ", " + Convert.ToInt32(this.CheckCloneFile) +
                                         ", " + Convert.ToInt32(FTPUsePassiveMode) +
                                         ", '" + this.DelforVerweis + "'" +


                                         ")";
            strSql = strSql + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                this.Fill();
            }
        }
        /// <summary>
        ///             Update Datensatz
        /// </summary>
        public bool Update()
        {
            string strSql = string.Empty;
            strSql = "Update Jobs SET " +
                                    "AdrVerweisID = " + AdrVerweisID +
                                    ", ASNFileTyp = '" + ASNFileTyp + "'" +
                                    ", ASNTypID = " + ASNTypID +
                                    ", Direction = '" + Direction + "'" +
                                    ", activ = " + Convert.ToInt32(activ) +
                                    ", Path = '" + Path + "'" +
                                    ", Filename = '" + FileName + "' " +
                                    ", ASNArtID = " + ASNArtID +
                                    ", PostBy = '" + PostBy + "' " +
                                    ", FTPServer ='" + FTPServer + "' " +
                                    ", FTPUser = '" + FTPUser + "' " +
                                    ", FTPPass ='" + FTPPass + "' " +
                                    ", FTPFileName = '" + FTPFileName + "' " +
                                    ", SearchFileName = '" + SearchFileName + "' " +
                                    ", MandantenID = " + MandantenID +
                                    ", ArbeitsbereichsID = " + ArbeitsbereichID +
                                    ", EinheitLM =  '" + EinheitLM + "'" +
                                    ", validFrom = '" + this.ValidFrom + "'" +
                                    ", VerweisVDA4905 = '" + VerweisVDA4905 + "'" +
                                    ", UseCRLF = " + Convert.ToInt32(this.UseCRLF) +
                                    ", CreateOdetteStart = " + Convert.ToInt32(this.CreateOdetteStart) +
                                    ", CheckTransferPath = '" + this.CheckTransferPath + "'" +
                                    ", ASNFileStorePath = '" + this.ASNFileStorePath + "'" +
                                    ", ErrorPath = '" + this.ErrorPath + "'" +
                                    ", TransferFileName = '" + this.TransferFileName + "'" +
                                    ", ActionDate = '" + this.ActionDate + "'" +
                                    ", Periode =  '" + this.Periode + "'" +
                                    ", ViewProzessName ='" + ViewProzessName + "'" +
                                    ", CheckCloneFilePath = '" + this.CheckCloneFilePath + "'" +
                                    ", CheckCloneFileName = '" + this.CheckCloneFileName + "'" +
                                    ", CheckCloneFile = " + Convert.ToInt32(this.CheckCloneFile) +
                                    ", FTPUsePassiveMode = " + Convert.ToInt32(this.FTPUsePassiveMode) +
                                    ", DelforVerweis = '" + this.DelforVerweis + "'" +
                                    " WHERE ID=" + this.ID + "; ";

            bool bReturn = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            string strSql = string.Empty;
            strSql = "DELETE Jobs WHERE ID=" + this.ID + "; ";
            bool bReturn = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
            return bReturn;
        }
        ///<summary>clsJobs / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable("Jobs");
            string strSql = string.Empty;
            strSql = "SELECT * FROM Jobs WHERE ID=" + ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Jobs");
            SetDatenToClass(ref dt);
        }
        ///<summary>clsJobs / FillByASNFileTypAndASNTyp</summary>
        ///<remarks></remarks>
        public void FillByASNFileTypAndASNTyp(string myFileTyp, decimal myASNTypID, decimal myASNArtId, decimal myAdrVerweisID, bool myIsWD)
        {
            DataTable dt = new DataTable("Jobs");
            string strSql = string.Empty;
            if (myIsWD)
            {
                strSql = "SELECT TOP(1) * FROM Jobs " +
                            "WHERE " +
                                    "UPPER(ASNFileTyp)='" + myFileTyp.ToUpper() + "' " +
                                    "AND ASNArtID=" + myASNArtId + " " +
                                    "AND Direction='OUT' " +
                                    "AND activ=1";
            }
            else
            {
                strSql = "SELECT * FROM Jobs " +
                                            "WHERE " +
                                                    "UPPER(ASNFileTyp)='" + myFileTyp.ToUpper() + "' " +
                                                    "AND ASNTypID=" + myASNTypID + " " +
                                                    "AND Direction='OUT' " +
                                                    "AND AdrVerweisID=" + myAdrVerweisID;
            }
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Jobs");
            SetDatenToClass(ref dt);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myFileTyp"></param>
        /// <param name="myQueue"></param>
        public void GetJobByQueue(string myFileTyp, clsQueue myQueue)
        {
            DataTable dt = new DataTable("Jobs");
            string strSql = string.Empty;
            strSql = "SELECT * FROM Jobs " +
                                        "WHERE " +
                                                " UPPER(ASNFileTyp)='" + myFileTyp.ToUpper() + "' " +
                                                " AND ASNTypID=" + (int)myQueue.ASNTypID + " " +
                                                " AND Direction='OUT' " +
                                                " AND AdrVerweisID=" + (int)myQueue.AdrVerweisID +
                                                " AND ArbeitsbereichsID= " + (int)myQueue.AbBereichID;

            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Jobs");
            SetDatenToClass(ref dt);
        }
        ///<summary>clsJobs / SetDatenToClass</summary>
        ///<remarks></remarks>
        private void SetDatenToClass(ref DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                this.ID = decTmp;

                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AdrVerweisID"].ToString(), out decTmp);
                this.AdrVerweisID = decTmp;

                this.ASNFileTyp = dt.Rows[i]["ASNFileTyp"].ToString();

                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ASNTypID"].ToString(), out decTmp);
                this.ASNTypID = decTmp;

                this.Direction = dt.Rows[i]["Direction"].ToString();
                this.activ = (bool)dt.Rows[i]["activ"];
                this.Path = dt.Rows[i]["Path"].ToString();
                this.FileName = dt.Rows[i]["FileName"].ToString();
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ASNArtID"].ToString(), out decTmp);
                this.ASNArtID = decTmp;
                this.PostBy = dt.Rows[i]["PostBy"].ToString();
                this.FTPServer = dt.Rows[i]["FTPServer"].ToString();
                this.FTPUser = dt.Rows[i]["FTPUser"].ToString();
                this.FTPPass = dt.Rows[i]["FTPPass"].ToString();
                this.FTPFileName = dt.Rows[i]["FTPFileName"].ToString();
                this.SearchFileName = dt.Rows[i]["SearchFileName"].ToString();
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["MandantenID"].ToString(), out decTmp);
                this.MandantenID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ArbeitsbereichsID"].ToString(), out decTmp);
                this.ArbeitsbereichID = decTmp;
                //--Einheit darf nicht leer sein Default Artikel
                if ((this.Direction.Equals("OUT")) && (dt.Rows[i]["EinheitLM"].ToString().Equals(string.Empty)))
                {
                    this.EinheitLM = "Artikel";
                }
                else
                {
                    this.EinheitLM = dt.Rows[i]["EinheitLM"].ToString();
                }
                DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                DateTime.TryParse(dt.Rows[i]["validFrom"].ToString(), out dtTmp);
                this.ValidFrom = dtTmp;
                this.VerweisVDA4905 = dt.Rows[i]["VerweisVDA4905"].ToString();
                this.UseCRLF = (bool)dt.Rows[i]["UseCRLF"];
                this.CreateOdetteStart = (bool)dt.Rows[i]["CreateOdetteStart"];
                this.CheckTransferPath = dt.Rows[i]["CheckTransferPath"].ToString();
                this.ASNFileStorePath = dt.Rows[i]["ASNFileStorePath"].ToString();
                this.ErrorPath = dt.Rows[i]["ErrorPath"].ToString();
                this.TransferFileName = dt.Rows[i]["TransferFileName"].ToString();
                dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                DateTime.TryParse(dt.Rows[i]["ActionDate"].ToString(), out dtTmp);
                this.ActionDate = dtTmp;
                this.Periode = dt.Rows[i]["Periode"].ToString();
                this.ViewProzessName = dt.Rows[i]["ViewProzessName"].ToString();

                this.CheckCloneFilePath = dt.Rows[i]["CheckCloneFilePath"].ToString();
                this.CheckCloneFileName = dt.Rows[i]["CheckCloneFileName"].ToString();
                this.CheckCloneFile = (bool)dt.Rows[i]["CheckCloneFile"];

                this.FTPUsePassiveMode = (bool)dt.Rows[i]["FTPUsePassiveMode"];
                this.DelforVerweis = dt.Rows[i]["DelforVerweis"].ToString();

                //meldungstyp
                AsnTyp = new clsASNTyp();
                AsnTyp.GL_User = this.GL_User;
                //AsnTyp.ID = this.ASNTypID;
                //AsnTyp.Fill();
                AsnTyp.TypID = (Int32)this.ASNTypID;
                AsnTyp.FillbyTypID();
            }
        }
        ///<summary>clsJobs / clsJobs</summary>
        ///<remarks>Ermittelt die aktiven / passiven Prozesse</remarks>
        public DataTable GetJobs(bool bActiv, string myDirection, bool bAbruf, bool bAll)
        {
            string strSQL = string.Empty;
            strSQL = "Select a.* " +
                                "From Jobs a " +
                                "INNER JOIN ASNTyp b ON b.TypId=a.ASNTypID ";
            if (!bAll)
            {
                if (bActiv)
                {
                    strSQL = strSQL + " WHERE a.activ=1 AND a.Direction='" + myDirection + "' ";
                }
                else
                {
                    strSQL = strSQL + " WHERE a.activ=0 AND a.Direction='" + myDirection + "' ";
                }
                if (bAbruf)
                {
                    strSQL = strSQL + " AND b.Typ IN ('AbA', 'TAA') ";
                }
                else
                {
                    strSQL = strSQL + " AND b.Typ NOT IN ('AbA', 'TAA') ";
                }
            }
            // DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "aktiveProzess");
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Jobs");
            return dt;
        }
        ///<summary>clsJobs / ConvertDataTableJobToList</summary>
        ///<remarks>Ermittelt die aktiven / passiven Prozesse</remarks>
        public List<clsJobs> ConvertDataTableJobToList(DataTable dt)
        {
            List<clsJobs> tmpList = new List<clsJobs>();
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsJobs tmpJob = new clsJobs();
                    tmpJob.InitClass(this.GLSystem, this.GL_User, false);
                    tmpJob.ID = decTmp;
                    tmpJob.Fill();
                    tmpList.Add(tmpJob);
                }
            }
            return tmpList;
        }
        /***************************************************************************************
        *                           public static procedure
        ****************************************************************************************/
        ///<summary>clsJobs / FillByASNFileTypAndASNTyp</summary>
        ///<remarks></remarks>
        public static decimal GetASNArtIDByAdress(Globals._GL_USER myUser, decimal myAdrID)
        {
            string strSql = string.Empty;
            strSql = "SELECT Top(1) ASNArtID FROM Jobs " +
                                        "WHERE AdrVerweisID=" + myAdrID + " ";

            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSql, myUser.User_ID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        ///<summary>clsJobs / FillByASNFileTypAndASNTyp</summary>
        ///<remarks></remarks>
        public static string GetASNFileTypByAdress(Globals._GL_USER myUser, decimal myAdrID, string myDirection)
        {
            string strSql = string.Empty;
            strSql = "SELECT Top(1) ASNFileTyp FROM Jobs " +
                                        "WHERE AdrVerweisID=" + myAdrID + " " +
                                        "AND Direction='" + myDirection + "' " +
                                        "AND  activ=1 ;";


            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSql, myUser.User_ID);
            return strTmp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myUser"></param>
        /// <param name="myDirection"></param>
        /// <returns></returns>
        public static List<clsJobs> GetJobByActionDateAndDirection(Globals._GL_SYSTEM myGLSystem, Globals._GL_USER myUser, string myDirection)
        {
            List<clsJobs> retList = new List<clsJobs>();
            List<int> listID = new List<int>();

            string strSql = string.Empty;
            strSql = //"SELECT Top(1) ID FROM Jobs " +
                         "SELECT ID FROM Jobs " +
                                            "WHERE " +
                                            "ActionDate<'" + DateTime.Now + "' " +
                                            "AND Direction='" + myDirection + "' " +
                                            " ANd activ=1 " +
                                            "AND ASNTypID IN(Select TypId FROM ASNTyp where Typ IN ('BML', 'BME')); ";
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, myUser.User_ID, "Jobs");
            foreach (DataRow row in dt.Rows)
            {
                string strTmp = row["ID"].ToString();
                int iTmp = 0;
                int.TryParse(strTmp, out iTmp);
                if (iTmp > 0)
                {
                    clsJobs tmpJob = new clsJobs();
                    tmpJob.InitClass(myGLSystem, myUser, false);
                    tmpJob.ID = (decimal)iTmp;
                    tmpJob.Fill();
                    retList.Add(tmpJob);
                }
            }
            return retList;
        }
        ///<summary>clsJobs / FillByASNFileTypAndASNTyp</summary>
        ///<remarks></remarks>
        public static DataTable GetJobsByAdress(Globals._GL_USER myUser, decimal myAdrID)
        {
            string strSql = string.Empty;
            strSql = "SELECT * FROM Jobs " +
                                        "WHERE AdrVerweisID=" + myAdrID + " ";

            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, myUser.User_ID, "JobByAdr");
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySourceAdrId"></param>
        /// <param name="myDestAdrId"></param>
        /// <param name="myUserId"></param>
        /// <returns></returns>
        public static bool InsertShapeByRefAdr(List<int> myListID, int myAdrId, decimal myUserId, decimal myAbBereichID, decimal myMandandtId)
        {
            bool bReturn = false;
            //Eintrag
            string strSql = string.Empty;
            strSql = "INSERT INTO [Jobs] ([AdrVerweisID],[ASNFileTyp] ,[ASNTypID],[Direction],[activ],[Path] ,[FileName] " +
                                        ",[ASNArtID],[PostBy],[FTPServer],[FTPUser],[FTPPass],[FTPFileName],[SearchFileName] " +
                                        ",[MandantenID],[ArbeitsbereichsID],[EinheitLM],[validFrom] ,[VerweisVDA4905] ,[UseCRLF] " +
                                        ",[CreateOdetteStart],[CheckTransferPath],[ASNFileStorePath],[ErrorPath] ,[TransferFileName] " +
                                        ",[ActionDate],[Periode]) " +
                    "SELECT " +
                            myAdrId +
                            ", a.ASNFileTyp " +
                            ", a.ASNTypID " +
                            ", a.Direction " +
                            ", a.activ " +
                            ", a.Path " +
                            ", a.FileName " +
                            ", a.ASNArtID " +
                            ", a.PostBy " +
                            ", a.FTPServer " +
                            ", a.FTPUser " +
                            ", a.FTPPass " +
                            ", a.FTPFileName " +
                            ", a.SearchFileName " +
                            ", " + (int)myMandandtId + //", a.MandantenID " +
                            ", " + (int)myAbBereichID + //", a.ArbeitsbereichsID " +
                            ", a.EinheitLM " +
                            ", '" + Globals.DefaultDateTimeMinValue + "'" +      //", a.validFrom " +
                            ", a.VerweisVDA4905 " +
                            ", a.UseCRLF " +
                            ", a.CreateOdetteStart " +
                            ", a.CheckTransferPath " +
                            ", a.ASNFileStorePath " +
                            ", a.ErrorPath " +
                            ", a.TransferFileName " +
                            ", a.ActionDate " +
                            ", a.Periode " +
                                //", a.ViewProzessName"+

                                " FROM Jobs a " +
                                    "WHERE " +
                                    " a.ID in (" + string.Join(",", myListID.ToArray()) + ");";
            bReturn = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "InsertJobs", myUserId);
            return bReturn;
        }
    }
}
