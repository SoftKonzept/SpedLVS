using LVS;
using Sped4.Classes.UpdateArchive;
using System;
using System.Data.SqlClient;
using System.Threading;

namespace Sped4.Classes
{
    class clsUpdateArchiv
    {
        ///<summary>
        ///             In dieser Klasse werden die Updates für das Programm Sped4 verwaltet.Beim Programmstart überprüft Sped4 
        ///             die aktuelle Version mit der Datenbankversion. Sind die Versionen unterschiedlich werden die entsprechenden
        ///             Update durchgeführt
        ///</summary>
        ///<remarks>C:\develop\comTEC\SpedLVS\Sped4\Classes\clsUpdate.cs
        ///             Die Versionnummer von Sped4 setzt sich folgendermaßen zusammen:
        ///             Die Versionsnummer von Sped4 ist 4-stellig (Bsp: 1.234)
        ///                 1. Stelle       : besondere / grundlegenede Erweiterungen
        ///                 2. Stelle       : Datenbankänderungen 
        ///                 3. + 4. Stelle  : kleinere Änderungen im Programm
        ///               
        ///             Das Array "UpdateArray" beinhaltet alle Versionen. Diese Array wird beim Update-Vorgang durchlaufen und mit 
        ///             der Datenbankversion verglichen und entsprechend das Update durchzuführen.
        ///</remarks>
        //************  User  ***************
        private decimal _BenutzerID;

        public decimal BenutzerID
        {
            get
            {
                return _BenutzerID;
            }
            set
            {
                _BenutzerID = value;
            }
        }

        internal clsSystem system;
        internal frmUpdateMirror upMirr;
        private decimal SoftwareVersion = 1001M;
        //public int[] UpdateArray =
        //{
        //    1400, 1401

        //};

        /*********************************************************************************************************/
        public decimal dbVersion { get; set; }

        public bool UpdateOK { get; set; }

        ///<summary>DoUpdate(string tmpSQL) / clsUpdate</summary>
        ///<remarks>Führt die SQL - Update-Anweisung aus.</remarks>
        ///<param name="tmpSQL">SQL-Anweisung</param>
        ///<returns>Rückgabe von TRUE / FALSE nach Durchführung der SQL-Anweisung</returns>
        private bool DoUpdate(string tmpSQL)
        {
            bool updateOK = true;

            SqlCommand UpCommand = new SqlCommand();
            Globals.SQLconArchive.Open();

            //Start Transaction
            SqlTransaction tAction;
            tAction = Globals.SQLconArchive.Connection.BeginTransaction("UpdateArchiv");

            UpCommand.Connection = Globals.SQLconArchive.Connection;
            UpCommand.Transaction = tAction;
            try
            {
                UpCommand.CommandText = tmpSQL;
                //Globals.SQLcon.Open();
                UpCommand.ExecuteNonQuery();

                tAction.Commit();

                UpCommand.Dispose();
                Globals.SQLconArchive.Close();
            }
            catch (Exception ex)
            {
                tAction.Rollback();

                //Add Logbucheintrag Exception
                string beschreibung = "Exception: " + ex.ToString();
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), beschreibung);
                clsMessages.Allgemein_ERRORTextShow(ex.ToString());
                updateOK = false;
            }
            tAction.Dispose();
            return updateOK;
        }

        public bool ExistDB()
        {
            bool bExist = false;
            string strSQL = UpdateArchive.ExistDB.SqlString();
            bExist = clsSQLARCHIVE.ExecuteSQL(strSQL, 1);
            return bExist;
        }
        //
        ///<summary>AddToLog(string strVersionsupdate) / clsUpdate</summary>
        ///<remarks>Die Update-Aktion wird im Logbuch dokumentiert.</remarks>
        ///<param name="strVersionsupdate">neue Versionsnummer nach dem Update</param>
        private void AddToLog(string strVersionsupdate)
        {
            string Beschreibung = "Software Update durchgeführt auf " + strVersionsupdate;
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
        }

        //
        ///<summaryGetSQLforUpdateVersion(string strVersion) / clsUpdate</summary>
        ///<remarks>Liefert die SQL-Anweisung zum Eintrag der neuen Versionsnummer in die Datenbank.</remarks>
        ///<param name="strVersion">neue Versionsnummer nach dem Update</param>
        private string GetSQLforUpdateVersion(string strVersion)
        {
            string sql = " Update Version SET " +
                          "Versionsnummer='" + strVersion + "' ";
            //int iTmp = 0;
            int.TryParse(strVersion, out int iTmp);
            if (iTmp > 1307)
            {
                sql += ", LastUpdate = '" + DateTime.Now + "'";

            }
            sql += "WHERE ID=1 ;";
            return sql;
        }

        //
        ///<summary>InitUpdate() / clsUpdate</summary>
        ///<remarks>Start der Updatefunktion.</remarks>
        public void InitUpdate()
        {
            BenutzerID = upMirr.GL_User.User_ID;
            UpdateOK = false;

            upMirr.strFortschritt = Environment.NewLine;
            upMirr.strFortschritt = "-------------------------------------------";
            upMirr.strFortschritt = Environment.NewLine;
            //string strMirrTextOld = upMirr.strFortschritt;

            //upMirr.strFortschritt = string.Empty;
            upMirr.strFortschritt = upMirr.strFortschritt.ToString().Trim() + "START Update DB ARCHIV" + Environment.NewLine;
            upMirr.SetInfoFortschritt();

            Thread.Sleep(2000);

            //DB-Version und SoftwareVersion werden verglichen
            system = new clsSystem();
            system.BenutzerID = this.BenutzerID;
            int[] UpdateArray = AppVersion.UpdateVersions();
            SoftwareVersion = Functions.GetMaxArray(UpdateArray);
            string strDBVersion = system.SystemVersionAppArchive.ToString();
            if ((system.SystemVersionAppDecimalArchive < SoftwareVersion) |
                (system.SystemVersionAppDecimalArchive == 0))
            {
                //Update Array wird durchlaufen und die DB-Version mit der 
                //entsprechenden UpdateArray-Version verglichen 
                for (Int32 i = 0; i <= UpdateArray.Length - 1; i++)
                {
                    bool bIsDefault = false;
                    bool boUpdateOK = false;
                    decimal decTmp = 0.0M;

                    string strTmp = UpdateArray[i].ToString();
                    Decimal.TryParse(strTmp, out decTmp);

                    //decTmp = Convert.ToDecimal(UpdateArray[i].ToString());

                    //Vergleich
                    if ((decTmp > 1308) && (system.SystemVersionAppDecimalArchive < decTmp))
                    {
                        //upMirr.strFortschritt = Environment.NewLine +
                        //                        upMirr.strFortschritt.ToString().Trim() +
                        //                        Environment.NewLine +
                        //                        "Installiere Update " +
                        //                        Functions.FormatDecimalVersion(decTmp) +
                        //                        Environment.NewLine;

                        //upMirr.strFortschritt = Environment.NewLine +
                        //                        upMirr.strFortschritt.ToString().Trim() +
                        //                        Environment.NewLine +
                        //                        DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") +
                        //                        ": Installiere Update Version -> " +
                        //                        Functions.FormatDecimalVersion(decTmp) +
                        //                        Environment.NewLine;
                        //upMirr.SetInfoFortschritt();


                        switch (decTmp.ToString())
                        {
                            case Aup1318.const_up1318:
                                SetUpdateVersionInfo(Functions.FormatDecimalVersion(decTmp));
                                boUpdateOK = DoUpdate(Aup1318.SqlString());
                                //Aup1316.SqlStringUpdate();
                                break;


                            case Aup1317.const_up1317:
                                SetUpdateVersionInfo(Functions.FormatDecimalVersion(decTmp));
                                boUpdateOK = DoUpdate(Aup1317.SqlString());
                                //Aup1316.SqlStringUpdate();
                                break;
                            case Aup1316.const_up1316:
                                SetUpdateVersionInfo(Functions.FormatDecimalVersion(decTmp));
                                boUpdateOK = DoUpdate(Aup1316.SqlString());
                                //Aup1316.SqlStringUpdate();
                                break;
                            case Aup1313.const_up1313:
                                SetUpdateVersionInfo(Functions.FormatDecimalVersion(decTmp));
                                boUpdateOK = DoUpdate(Aup1313.SqlString());
                                Aup1313.SqlStringUpdate_WorkspaceId();
                                break;
                            case Aup1311.const_up1311:
                                SetUpdateVersionInfo(Functions.FormatDecimalVersion(decTmp));
                                boUpdateOK = DoUpdate(Aup1311.SqlString());
                                //boUpdateOK = DoUpdate(Aup1310.SqlStringUpdate_InsertFirstRow());
                                break;

                            case Aup1310.const_up1310:
                                SetUpdateVersionInfo(Functions.FormatDecimalVersion(decTmp));
                                boUpdateOK = DoUpdate(Aup1310.SqlString());
                                //boUpdateOK = DoUpdate(Aup1310.SqlStringUpdate_InsertFirstRow());
                                break;

                            case Aup1309.const_up1309:
                                SetUpdateVersionInfo(Functions.FormatDecimalVersion(decTmp));
                                boUpdateOK = DoUpdate(Aup1309.SqlString());
                                boUpdateOK = DoUpdate(Aup1309.SqlStringUpdate_InsertFirstRow());
                                break;

                            default:
                                boUpdateOK = true;
                                bIsDefault = true;
                                system.SystemVersionAppDecimalArchive = decTmp;
                                break;
                        }

                        //if ((boUpdateOK) && (VersionARCHIVViewModel.ExistTable()))
                        if (boUpdateOK) //Update OK 
                        {
                            //UpdateFUnktion wird ausgeführt und das Update im Logbuch eingetragen
                            DoUpdate(GetSQLforUpdateVersion(decTmp.ToString()));
                            AddToLog(decTmp.ToString());
                            SetMessageUpdateOK(Functions.FormatDecimalVersion(decTmp));
                        }
                        else
                        {
                            if (!bIsDefault)
                            {
                                SetMessageUpdateFailed(Functions.FormatDecimalVersion(decTmp));
                                i = UpdateArray.Length;
                            }
                        }
                        ////neue Versionnummer aus DB auslesen
                        //strDBVersion = system.SystemVersionAppArchive.ToString();

                        //// - nach Update aller Archiv Updates prüfen, ob die neue DB Version 
                        //if (system.SystemVersionAppDecimalArchive < system.SystemVersionAppDecimal)
                        //{
                        //    DoUpdate(GetSQLforUpdateVersion(system.SystemVersionAppDecimal.ToString()));
                        //    AddToLog(decTmp.ToString());
                        //    SetMessageUpdateOK(Functions.FormatDecimalVersion(decTmp));
                        //}
                        upMirr.VisibleStartUpdateButton(false);
                        UpdateOK = boUpdateOK;
                    }
                }
                //neue Versionnummer aus DB auslesen
                strDBVersion = system.SystemVersionAppArchive.ToString();

                // - nach Update aller Archiv Updates prüfen, ob die neue DB Version 
                if (system.SystemVersionAppDecimalArchive < system.SystemVersionAppDecimal)
                {
                    DoUpdate(GetSQLforUpdateVersion(system.SystemVersionAppDecimal.ToString()));
                    strDBVersion = system.SystemVersionAppArchive.ToString();
                    AddToLog(system.SystemVersionAppDecimalArchive.ToString());
                    SetMessageUpdateOK(Functions.FormatDecimalVersion(system.SystemVersionAppDecimalArchive));
                }

            }

            //upMirr.strFortschritt = Environment.NewLine + strMirrTextOld;  
            //upMirr.SetInfoFortschritt();
        }

        private void SetUpdateVersionInfo(string strVersion)
        {
            upMirr.strFortschritt = Environment.NewLine +
                                    upMirr.strFortschritt.ToString().Trim() +
                                    Environment.NewLine +
                                    DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Installiere Update Version -> " + strVersion +
                                    Environment.NewLine;
            upMirr.SetInfoFortschritt();
        }


        ///<summary>SetMessageUpdateOK(string strVersion) / clsUpdate </summary>
        ///<remarks>Ausgabe der Info, dass das Update erfolgreich durchgeführt wurde.</remarks>
        private void SetMessageUpdateOK(string strVersion)
        {
            upMirr.strFortschritt = Environment.NewLine +
                                    upMirr.strFortschritt.ToString().Trim() +
                                    Environment.NewLine +
                                    "Update auf  Archiv - Version " + strVersion + " erfolgreich durchgeführt!" +
                                    Environment.NewLine;
            upMirr.SetInfoFortschritt();
        }

        ///<summary>SetMessageUpdateOK(string strVersion) / clsUpdate </summary>
        ///<remarks>Ausgabe der Info, dass es beim Update zu einem Fehler gekommen ist und das Update nicht erfolgreich durchgeführt werden konnte.</remarks>
        private void SetMessageUpdateFailed(string strVersion)
        {
            upMirr.strFortschritt = Environment.NewLine +
                                    upMirr.strFortschritt.ToString().Trim() +
                                    Environment.NewLine +
                                    "Update auf  Archiv - Version " + strVersion + " ist fehlgeschlagen. Bitte starten Sie Sped4 erneut!" +
                                    Environment.NewLine;
            upMirr.SetInfoFortschritt();
        }
    }
}
