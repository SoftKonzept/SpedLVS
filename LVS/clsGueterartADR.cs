using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsGueterartADR
    {
        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER _GL_User;
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

        //Table KundenTarife
        public decimal ID { get; set; }
        public decimal AbBereichID { get; set; }
        public decimal GArtID { get; set; }
        public decimal AdrID { get; set; }

        private bool _IsAssign;
        public bool IsAssign
        {
            get
            {
                _IsAssign = CheckGartIsAssign();
                return _IsAssign;
            }
            set { _IsAssign = value; }
        }
        private List<decimal> _ListAssignADR;
        public List<decimal> ListAssignADR
        {
            get
            {
                _ListAssignADR = GetAssignADR();
                return _ListAssignADR;
            }
            set { _ListAssignADR = value; }
        }
        public Dictionary<decimal, List<decimal>> DictAdrListGArten { get; set; }
        public Dictionary<decimal, List<decimal>> DictAdrListGArtenActiv { get; set; }
        public Dictionary<decimal, List<clsGut>> DictAdrClsGut { get; set; }

        public string AssignAdrsAsString
        {
            get
            {
                string strSQL = "Select " +
                                    "STUFF((SELECT ', '+ adr.ViewID " +
                                                  "FROM GueterartADR ga " +
                                                  "INNER JOIN Gueterart g on g.ID=ga.GArtID " +
                                                  "INNER JOIN ADR adr on adr.ID=ga.AdrID " +
                                                  "WHERE g.ID=a.ID Order by adr.ViewID FOR XML PATH ('')) " +
                                                  ",1,2,'') " +
                                    "from Gueterart a " +
                                    "WHERE a.ID=" + this.GArtID + ";";
                string strReturn = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                return strReturn;
            }
        }

        /************************************************************************************
         *                      Methoden
         * *********************************************************************************/
        ///<summary>clsGueterartADR / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER GLUser, Globals._GL_SYSTEM GLSystem)
        {
            this._GL_User = GLUser;
            this.GLSystem = GLSystem;
            this.AbBereichID = this.GLSystem.sys_ArbeitsbereichID;
        }
        ///<summary>clsGueterartADR / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            try
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO GueterartADR (GArtID, AdrID, AbBereichID) " +
                                               "VALUES (" +
                                                         (Int32)GArtID +
                                                         ", " + (Int32)AdrID +
                                                         ", " + (Int32)AbBereichID +
                                                        ")";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            catch (Exception ex)
            {
            }
        }
        ///<summary>clsGueterartADR / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            try
            {
                string strSql = string.Empty;
                strSql = "Delete GueterartADR WHERE GArtID=" + this.GArtID + " " +
                                                    "AND AbBereichID=" + this.AbBereichID + " " +
                                                    "AND AdrID=" + this.AdrID + "; ";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            catch (Exception ex)
            {
            }
        }
        ///<summary>clsGueterartADR / CheckTarifIsAssign</summary>
        ///<remarks></remarks>
        private bool CheckGartIsAssign()
        {
            bool RetVal = false;
            string strSQL = string.Empty;
            strSQL = "Select Count(*) as Anzahl  FROM GueterartADR " +
                                        "WHERE GArtID=" + this.GArtID + " " +
                                               "AND AbBereichID=" + this.AbBereichID + " " +
                                               "AND AdrID=" + this.AdrID + "; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                RetVal = true;
            }
            return RetVal;
        }
        ///<summary>clsGueterartADR / CheckTarifIsAssign</summary>
        ///<remarks></remarks>
        private List<decimal> GetAssignADR()
        {
            List<decimal> ListReturn = new List<decimal>();
            string strSQL = string.Empty;
            strSQL = "Select AdrID FROM GueterartADR " +
                                        "WHERE GArtID=" + this.GArtID + " " +
                                               "AND AbBereichID=" + this.AbBereichID + " ";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "GutADR");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AdrID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    ListReturn.Add(decTmp);
                }
            }
            return ListReturn;
        }
        ///<summary>clsGueterartADR / FIllDictionaries</summary>
        ///<remarks></remarks>
        public void FillDictionaries()
        {
            if (this.AbBereichID > 0)
            {
                DictAdrListGArten = new Dictionary<decimal, List<decimal>>();
                DictAdrListGArtenActiv = new Dictionary<decimal, List<decimal>>();
                List<decimal> ListGArt = new List<decimal>();
                List<decimal> ListGArtActiv = new List<decimal>();

                DictAdrClsGut = new Dictionary<decimal, List<clsGut>>();
                List<clsGut> ListClsGut = new List<clsGut>();
                clsGut tmpGut = new clsGut();

                string strSQL = string.Empty;
                strSQL = "Select AdrID, GArtID FROM GueterartADR " +
                                            "WHERE AbBereichID=" + this.AbBereichID + " " +
                                            //" AND AdrID in (7,57) "+  // test
                                            "Group by AdrID, GArtID ";

                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "GutADR");
                decimal decOldAdrID = 0;

                DataTable dtAdr = dt.DefaultView.ToTable(true, "AdrID");
                foreach (DataRow row in dtAdr.Rows)
                {
                    ListClsGut = new List<clsGut>();
                    ListGArt = new List<decimal>();
                    ListGArtActiv = new List<decimal>();

                    int iTmpAdrID = 0;
                    int.TryParse(row["AdrID"].ToString(), out iTmpAdrID);
                    if (iTmpAdrID > 0)
                    {
                        DataTable dtAdrGArten = dt.DefaultView.ToTable().Select("AdrID=" + iTmpAdrID).CopyToDataTable();
                        foreach (DataRow tmpRow in dtAdrGArten.Rows)
                        {
                            int iGArtID = 0;
                            int.TryParse(tmpRow["GArtID"].ToString(), out iGArtID);
                            if (iGArtID > 0)
                            {
                                ListGArt.Add((decimal)iGArtID);
                                tmpGut = new clsGut();
                                tmpGut.InitClass(this._GL_User, this.GLSystem);
                                tmpGut.ID = (decimal)iGArtID;
                                tmpGut.Fill();
                                ListClsGut.Add(tmpGut);
                                if (tmpGut.Aktiv)
                                {
                                    ListGArtActiv.Add((decimal)iGArtID);
                                }
                            }
                        }

                        if (!DictAdrListGArten.ContainsKey(iTmpAdrID))
                        {
                            DictAdrListGArten.Add(iTmpAdrID, ListGArt);
                        }
                        if (!DictAdrListGArtenActiv.ContainsKey(iTmpAdrID))
                        {
                            DictAdrListGArtenActiv.Add(iTmpAdrID, ListGArtActiv);
                        }
                        if (!DictAdrClsGut.ContainsKey(iTmpAdrID))
                        {
                            DictAdrClsGut.Add(iTmpAdrID, ListClsGut);
                        }
                    }
                }





                //for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                //{
                //    decimal decTmpAdrID = 0;
                //    Decimal.TryParse(dt.Rows[i]["AdrID"].ToString(), out decTmpAdrID);
                //    if (decTmpAdrID > 0)
                //    {
                //        if (decTmpAdrID == 57)
                //        {
                //            string str = "STOPP";
                //        }
                //        //der erste
                //        if (i == 0)
                //        {
                //            decimal decTmpGut = 0;
                //            Decimal.TryParse(dt.Rows[i]["GArtID"].ToString(), out decTmpGut);
                //            if (decTmpGut > 0)
                //            {
                //                ListGArt.Add(decTmpGut);

                //                tmpGut = new clsGut();
                //                tmpGut.InitClass(this._GL_User, this.GLSystem);
                //                tmpGut.ID = decTmpGut;
                //                tmpGut.Fill();
                //                ListClsGut.Add(tmpGut);
                //                if (tmpGut.Aktiv)
                //                {
                //                    ListGArtActiv.Add(decTmpGut);
                //                }
                //                decOldAdrID = decTmpAdrID;

                //                //wenn nur ein Datensatz
                //                if (dt.Rows.Count == 1)
                //                {
                //                    DictAdrListGArten.Add(decOldAdrID, ListGArt);
                //                    DictAdrListGArtenActiv.Add(decOldAdrID, ListGArtActiv);
                //                    DictAdrClsGut.Add(decOldAdrID, ListClsGut);
                //                }
                //            }
                //        }
                //        //letzte Datensatz 
                //        //Listen müssen dem DIct hinzugefügt werden
                //        else if (i == dt.Rows.Count - 1)
                //        {
                //            decimal decTmpGut = 0;
                //            Decimal.TryParse(dt.Rows[i]["GArtID"].ToString(), out decTmpGut);
                //            if (decTmpGut > 0)
                //            {
                //                ListGArt.Add(decTmpGut);

                //                tmpGut = new clsGut();
                //                tmpGut.InitClass(this._GL_User, this.GLSystem);
                //                tmpGut.ID = decTmpGut;
                //                tmpGut.Fill();
                //                if (tmpGut.Aktiv)
                //                {
                //                    ListGArtActiv.Add(decTmpGut);
                //                }
                //                ListClsGut.Add(tmpGut);

                //                decOldAdrID = decTmpAdrID;
                //            }
                //            DictAdrListGArten.Add(decOldAdrID, ListGArt);
                //            DictAdrListGArtenActiv.Add(decOldAdrID, ListGArtActiv);
                //            DictAdrClsGut.Add(decOldAdrID, ListClsGut);
                //        }
                //        else
                //        {
                //            if (decOldAdrID == decTmpAdrID)
                //            {
                //                decimal decTmpGut = 0;
                //                Decimal.TryParse(dt.Rows[i]["GArtID"].ToString(), out decTmpGut);
                //                if (decTmpGut > 0)
                //                {
                //                    ListGArt.Add(decTmpGut);

                //                    tmpGut = new clsGut();
                //                    tmpGut.InitClass(this._GL_User, this.GLSystem);
                //                    tmpGut.ID = decTmpGut;
                //                    tmpGut.Fill();
                //                    ListClsGut.Add(tmpGut);
                //                    if (tmpGut.Aktiv)
                //                    {
                //                        ListGArtActiv.Add(decTmpGut);
                //                    }
                //                    decOldAdrID = decTmpAdrID;
                //                }
                //            }
                //            else
                //            {
                //                //DictAdrListGArten.Add(decOldAdrID, ListGArt);
                //                //DictAdrListGArtenActiv.Add(decOldAdrID, ListGArtActiv);
                //                if (!DictAdrListGArten.ContainsKey(decOldAdrID))
                //                {
                //                    DictAdrListGArten.Add(decOldAdrID, ListGArt);
                //                }
                //                if (!DictAdrListGArtenActiv.ContainsKey(decOldAdrID))
                //                {
                //                    DictAdrListGArtenActiv.Add(decOldAdrID, ListGArtActiv);
                //                }
                //                ListGArt = new List<decimal>();
                //                ListGArtActiv = new List<decimal>();

                //                //DictAdrClsGut.Add(decOldAdrID, ListClsGut);
                //                if (!DictAdrClsGut.ContainsKey(decOldAdrID))
                //                {
                //                    DictAdrClsGut.Add(decOldAdrID, ListClsGut);
                //                }
                //                ListClsGut = new List<clsGut>();

                //                decimal decTmpGut = 0;
                //                Decimal.TryParse(dt.Rows[i]["GArtID"].ToString(), out decTmpGut);
                //                if (decTmpGut > 0)
                //                {
                //                    ListGArt.Add(decTmpGut);

                //                    tmpGut = new clsGut();
                //                    tmpGut.InitClass(this._GL_User, this.GLSystem);
                //                    tmpGut.ID = decTmpGut;
                //                    tmpGut.Fill();
                //                    if (tmpGut.Aktiv)
                //                    {
                //                        ListGArtActiv.Add(decTmpGut);
                //                    }
                //                    ListClsGut.Add(tmpGut);

                //                    decOldAdrID = decTmpAdrID;
                //                }
                //            }
                //        }
                //    }
                //}
            }
        }
        ///<summary>clsGueterartADR / GetAssignADRTable</summary>
        ///<remarks></remarks>
        public DataTable GetAssignADRTable()
        {
            string strSQL = string.Empty;
            strSQL = "Select a.ID as AdrID, a.ViewID, a.Name1 " +
                                            "FROM Gueterart g " +
                                            "INNER JOIN GueterartADR ga ON ga.GArtID=g.ID " +
                                            "INNER JOIN ADR a ON a.ID=ga.AdrID " +
                                            "WHERE " +
                                                "GArtID=" + this.GArtID + " " +
                                                "AND AbBereichID=" + this.AbBereichID + " ";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "GutADR");
            return dt;
        }
        ///<summary>clsGueterartADR / GetAssignADRTable</summary>
        ///<remarks></remarks>
        public DataTable GetAssignGArteToADR(decimal myAdrID)
        {
            string strSQL = string.Empty;
            strSQL = "Select g.ID as GArtID " +
                            ", g.Bezeichnung " +
                                            "FROM Gueterart g " +
                                            "INNER JOIN GueterartADR ga ON ga.GArtID=g.ID " +
                                            "INNER JOIN ADR a ON a.ID=ga.AdrID " +
                                            "WHERE " +
                                                "a.ID=" + myAdrID + " " +
                                                "AND ga.AbBereichID=" + this.AbBereichID +
                                                " AND g.aktiv=1 ";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ADRGut");
            return dt;
        }




    }
}
