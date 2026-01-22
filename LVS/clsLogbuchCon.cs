using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;

namespace LVS
{
    public class clsLogbuchCon
    {
        public const string const_Logbuch_Trennzeichen = "---------------------------------------------------------------------";
        public const string const_LogASNReadFileName = "LogIN";
        public const string const_LogASNWriteFileName = "LogOUT";
        public const string const_LogCreateEAFileName = "LogEA";
        public const string const_LogSystem = "LogSys";
        public const string const_LogCronJob = "LogCronJob";
        public const string const_LogCustomProcess = "LogCustomProcess";
        public const string const_LogTaskExcecution = "LogTaskExecution";

        public const string const_Task_ASNRead = "ASNRead";
        public const string const_Task_ASNWrite = "ASNWrite";
        public const string const_Task_CALLRead = "CALLRead";
        public const string const_Task_CreateEA = "CreateEA";
        public const string const_Task_CronJob = "CronJob";
        public const string const_Task_CustomProcess = "CustomProcess";
        public const string const_Task_System = "System";




        public LVS.Globals._GL_USER GL_User;
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


        public decimal ID { get; set; }
        public DateTime Datum { get; set; }
        public string Typ { get; set; }
        public string LogText { get; set; }
        public string TableName { get; set; }
        public decimal TableID { get; set; }

        /***************************************************************************************
         *                          Methoden
         * ************************************************************************************/
        ///<summary>Globals / GetAddLogbuchSQLString</summary>
        ///<remarks></remarks>
        public string GetAddLogbuchSQLString()
        {
            string strSQL = string.Empty;

            strSQL = "INSERT INTO Logbuch (Datum, Typ, LogText, TableName, TableID) " +
                            "VALUES ('" + DateTime.Now + "' " +
                                   ",'" + Typ + "' " +
                                   ", '" + LogText + "'" +
                                   ",'" + TableName + "' " +
                                   "," + TableID +
                                   ")";

            return strSQL;
        }
        ///<summary>Globals / Add</summary>
        ///<remarks></remarks>
        public void Add(string mySQLString)
        {
            clsSQLCOM.ExecuteSQLWithTRANSACTION(mySQLString, "LogAdd", BenutzerID);
        }
        ///<summary>Globals / GetLogbuch</summary>
        ///<remarks></remarks>
        public DataTable GetLogbuch(bool boAll, DateTime dtVon, DateTime dtBis)
        {
            DataTable dt = new DataTable("Logbuch");
            string sql = string.Empty;
            dt.Clear();
            string strSQL = string.Empty;
            if (boAll)
            {
                strSQL = "SELECT * FROM Logbuch  ORDER BY Datum;";
            }
            else
            {
                //strSQL = "SELECT TOP(30) * FROM Logbuch ORDER BY Datum DESC;";
                strSQL = "SELECT * FROM Logbuch WHERE Datum>='" + dtVon + "' AND Datum<'" + dtBis.AddDays(1) + "' " +
                                                                             "ORDER BY Datum Desc";
            }
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Logbuch");
            return dt;
        }
        ///<summary>Globals / AddCollectedSQLStringToLog</summary>
        ///<remarks></remarks>
        public static void AddCollectedSQLStringToLog(List<string> mySqlList, Globals._GL_USER myGLUser)
        {
            string strSql = string.Join(" ", mySqlList.ToArray());
            bool bOK = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "LogInsert", myGLUser.User_ID);
        }
        ///<summary>Globals / WriteLogToFile</summary>
        ///<remarks></remarks>
        public static void WriteLogToFile(ref List<string> myLogList, clsSystem mySys, string myLogPath, string myTask)
        {
            List<string> listWriteTo = new List<string>();

            //Filename nach Taskart
            string strLogFileName = clsViewLog.GetLogViewFileDateTimeMask();
            //string strLogFileName = DateTime.Now.ToString("yyyy") + "_" +
            //                        DateTime.Now.ToString("MM") + "_" +
            //                        DateTime.Now.ToString("dd") + "_";
            //Auswahl der korrekten Dateiendung
            switch (myTask)
            {
                case clsLogbuchCon.const_Task_ASNRead:
                    strLogFileName = strLogFileName + clsLogbuchCon.const_LogASNReadFileName + ".txt";
                    break;
                case clsLogbuchCon.const_Task_ASNWrite:
                    strLogFileName = strLogFileName + clsLogbuchCon.const_LogASNWriteFileName + ".txt";
                    break;
                case clsLogbuchCon.const_Task_CreateEA:
                    strLogFileName = strLogFileName + clsLogbuchCon.const_LogCreateEAFileName + ".txt";
                    break;
                case clsLogbuchCon.const_Task_System:
                    strLogFileName = strLogFileName + clsLogbuchCon.const_LogSystem + ".txt";
                    break;
                case clsLogbuchCon.const_Task_CronJob:
                    strLogFileName = strLogFileName + clsLogbuchCon.const_LogCronJob + ".txt";
                    break;
                case clsLogbuchCon.const_Task_CustomProcess:
                    strLogFileName = strLogFileName + clsLogbuchCon.const_LogCustomProcess + ".txt";
                    break;
                case clsLogbuchCon.const_LogTaskExcecution:
                    strLogFileName = strLogFileName + clsLogbuchCon.const_LogTaskExcecution + ".txt";
                    break;

                default:
                    strLogFileName = string.Empty;
                    break;
            }

            if (strLogFileName != string.Empty)
            {
                string strPath = myLogPath + clsLogbuchCon.CreateLogPath();
                string fileNamePath = strPath + strLogFileName;

                //check logordner
                Functions.CheckDirectory(strPath);

                //check logdatei
                if (
                        (File.Exists(fileNamePath))
                        //&& (helper_IOFile.CanOpenFile(fileNamePath))
                        && (helper_IOFile.IsFileAvailable(fileNamePath))
                   )
                {
                    //StreamReader readFile = new StreamReader(strPath + strLogFileName);
                    StreamReader readFile = new StreamReader(fileNamePath);
                    string strLine = string.Empty;
                    while ((strLine = readFile.ReadLine()) != null)
                    {
                        if (strLine != string.Empty)
                        {
                            listWriteTo.Add(strLine);
                        }
                    }
                    readFile.Close();
                }
                else
                {
                    var crfile = File.Create(fileNamePath);
                    crfile.Close();
                }
                //ListToWrite füllen
                for (Int32 i = 0; i <= myLogList.Count - 1; i++)
                {
                    listWriteTo.Add(myLogList[i].ToString());
                }
                myLogList.Clear();

                //schreiben wenn listToWrite gefüllt
                if (listWriteTo.Count > 0)
                {
                    bool LogIsWriten = true;
                    int iCheckCount = 0;

                    while (iCheckCount < 3)
                    {
                        if (helper_IOFile.CanOpenFile(fileNamePath))
                        {
                            //iCheckCount = 3;
                            string strToWrite = String.Join(Environment.NewLine, listWriteTo.ToArray());
                            try
                            {
                                using (StreamWriter file = new StreamWriter(fileNamePath))
                                {
                                    file.WriteLine(strToWrite);
                                    file.Close();
                                }
                                iCheckCount = 3;
                            }
                            catch (Exception ex)
                            {
                                LogIsWriten = false;
                                iCheckCount++;
                                try
                                {
                                    clsError Error = new clsError();
                                    Error.Aktion = "clsLogbuchCon - WriteLogToASNReadWriteFile() ";
                                    Error.Datum = DateTime.Now;
                                    Error.ErrorText = "try Catch Exception / Writing"; // string.Empty;
                                    Error.exceptText = ex.ToString();
                                    Error.WriteError();

                                    clsMail ErrorMail = new clsMail();
                                    ErrorMail.InitClass(new Globals._GL_USER(), null);
                                    ErrorMail.Subject = "Error - clsLogbuchCon - WriteLogToASNReadWriteFile()";
                                    string strMes = "clsLogbuchCon - WriteLogToASNReadWriteFile() " + Environment.NewLine;
                                    //strMes += "ExceptionObject: " + ex.ExceptionObject.ToString() + Environment.NewLine;
                                    strMes += "e.ToString(): " + ex.ToString() + Environment.NewLine;
                                    strMes += "iCount: " + iCheckCount.ToString() + Environment.NewLine;
                                    strMes += "Zeit: " + DateTime.Now.ToString() + Environment.NewLine;
                                    ErrorMail.Message = strMes;
                                    ErrorMail.SendError();
                                }
                                catch (Exception ex1)
                                { }
                                finally
                                {
                                    Thread.Sleep(6000);
                                    if (LogIsWriten)
                                    {
                                        myLogList.Clear();
                                    }
                                }
                            }
                        }
                        else
                        {
                            iCheckCount++;
                            if (iCheckCount == 3)
                            {
                                fileNamePath = strPath + DateTime.Now.ToString("HHmmss") + "#" + strLogFileName;

                                string strToWrite = String.Join(Environment.NewLine, listWriteTo.ToArray());
                                try
                                {
                                    using (StreamWriter file = new StreamWriter(fileNamePath))
                                    {
                                        file.WriteLine(strToWrite);
                                        file.Close();
                                    }
                                    iCheckCount = 3;
                                }
                                catch (Exception ex)
                                {
                                    LogIsWriten = false;
                                    iCheckCount++;
                                    try
                                    {
                                        clsError Error = new clsError();
                                        Error.Aktion = "clsLogbuchCon - WriteLogToASNReadWriteFile() ";
                                        Error.Datum = DateTime.Now;
                                        Error.ErrorText = "try Catch Exception / Writing"; // string.Empty;
                                        Error.exceptText = ex.ToString();
                                        Error.WriteError();

                                        clsMail ErrorMail = new clsMail();
                                        ErrorMail.InitClass(new Globals._GL_USER(), null);
                                        ErrorMail.Subject = "Error - clsLogbuchCon - WriteLogToASNReadWriteFile()";
                                        string strMes = "clsLogbuchCon - WriteLogToASNReadWriteFile() " + Environment.NewLine;
                                        //strMes += "ExceptionObject: " + ex.ExceptionObject.ToString() + Environment.NewLine;
                                        strMes += "e.ToString(): " + ex.ToString() + Environment.NewLine;
                                        strMes += "iCount: " + iCheckCount.ToString() + Environment.NewLine;
                                        strMes += "Zeit: " + DateTime.Now.ToString() + Environment.NewLine;
                                        ErrorMail.Message = strMes;
                                        ErrorMail.SendError();
                                    }
                                    catch (Exception ex1)
                                    { }
                                    finally
                                    {
                                        Thread.Sleep(6000);
                                        if (LogIsWriten)
                                        {
                                            myLogList.Clear();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Thread.Sleep(6000);
                            }
                        }
                    }


                    //bool LogIsWriten = true;
                    //Log an Datei anfügen
                    //StreamWriter file = new StreamWriter(strPath + strLogFileName);
                    //string strToWrite = String.Join(Environment.NewLine, listWriteTo.ToArray());
                    //try
                    //{
                    //    using (StreamWriter file = new StreamWriter(strPath + strLogFileName))
                    //    {
                    //        file.WriteLine(strToWrite);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    LogIsWriten = true;
                    //    try
                    //    {
                    //        clsError Error = new clsError();
                    //        Error.Aktion = "clsLogbuchCon - WriteLogToASNReadWriteFile() ";
                    //        Error.Datum = DateTime.Now;
                    //        Error.ErrorText = string.Empty;
                    //        Error.exceptText = ex.ToString();
                    //        Error.WriteError();
                    //    }
                    //    catch (Exception ex1)
                    //    { }
                    //}
                    //finally
                    //{
                    //    if (LogIsWriten)
                    //    {
                    //        myLogList.Clear();
                    //    }
                    //}
                    //file.Close();
                }
            }
        }
        ///<summary>Globals / CreateLogPath</summary>
        ///<remarks></remarks>
        private static string CreateLogPath()
        {
            string strPath = "\\Log\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\";
            return strPath;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySys"></param>
        /// <param name="myLogPath"></param>
        /// <param name="myTask"></param>
        /// <returns></returns>
        public static List<clsViewLog> GetLogText(clsSystem mySys, string myLogPath, string myTask)
        {
            List<clsViewLog> listReturn = new List<clsViewLog>();
            List<string> listWriteTo = new List<string>();
            try
            {
                //Filename nach Taskart
                string strLogFileName = DateTime.Now.ToString("yyyy") + "_" +
                                        DateTime.Now.ToString("MM") + "_" +
                                        DateTime.Now.ToString("dd") + "_";
                //Auswahl der korrekten Dateiendung
                switch (myTask)
                {
                    case clsLogbuchCon.const_Task_ASNRead:
                        strLogFileName = strLogFileName + clsLogbuchCon.const_LogASNReadFileName + ".txt";
                        break;
                    case clsLogbuchCon.const_Task_ASNWrite:
                        strLogFileName = strLogFileName + clsLogbuchCon.const_LogASNWriteFileName + ".txt";
                        break;
                    case clsLogbuchCon.const_Task_CreateEA:
                        strLogFileName = strLogFileName + clsLogbuchCon.const_LogCreateEAFileName + ".txt";
                        break;
                    case clsLogbuchCon.const_Task_System:
                        strLogFileName = strLogFileName + clsLogbuchCon.const_LogSystem + ".txt";
                        break;
                    case clsLogbuchCon.const_Task_CronJob:
                        strLogFileName = strLogFileName + clsLogbuchCon.const_LogCronJob + ".txt";
                        break;
                    case clsLogbuchCon.const_Task_CustomProcess:
                        strLogFileName = strLogFileName + clsLogbuchCon.const_LogCustomProcess + ".txt";
                        break;
                    default:
                        strLogFileName = string.Empty;
                        break;
                }

                if (strLogFileName != string.Empty)
                {
                    string strPath = myLogPath + clsLogbuchCon.CreateLogPath();
                    //check logordner
                    Functions.CheckDirectory(strPath);

                    //check logdatei
                    if (File.Exists(strPath + strLogFileName))
                    {
                        StreamReader readFile = new StreamReader(strPath + strLogFileName);
                        string strLine = string.Empty;

                        while ((strLine = readFile.ReadLine()) != null)
                        {
                            if (strLine != string.Empty)
                            {
                                listWriteTo.Add(strLine);
                            }
                        }
                        readFile.Close();
                    }
                }

                for (int i = listWriteTo.Count - 1; i >= listWriteTo.Count - 50; i--)
                {
                    if (i == 1)
                    {
                        //test
                        string str = string.Empty;
                    }
                    try
                    {
                        string strLine = listWriteTo[i];
                        if (!strLine.Equals(string.Empty))
                        {
                            listReturn.Add(clsViewLog.GetViewLogItem(strLine));
                        }
                    }
                    catch (Exception ex)
                    {
                        string strError = ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string strError = ex.ToString();
            }

            return listReturn;
        }
    }
}
