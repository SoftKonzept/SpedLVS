using System;
using System.Data;

namespace LVS.Helper
{
    public class helper_ArtikelArbeitsbereichCheck
    {
        internal clsSystem Sys;
        public clsArtikel ArtCheck;
        public clsArbeitsbereiche Arbeitsbereich;
        internal Globals._GL_USER GLUser;
        internal Globals._GL_SYSTEM GLSystem;
        public string InfoText = string.Empty;
        public bool FindInSameWorkspace = false;





        public helper_ArtikelArbeitsbereichCheck(decimal myArtId, clsSystem mySys, Globals._GL_SYSTEM myGLSys, Globals._GL_USER myGLUser)
        {
            this.Sys = mySys;
            this.GLSystem = myGLSys;
            this.GLUser = myGLUser;

            if (
                    (this.Sys is clsSystem) &&
                    (myArtId > 0)
                )
            {
                this.Sys = mySys;

                ArtCheck = new clsArtikel();
                ArtCheck.InitClass(this.GLUser, this.GLSystem);
                ArtCheck.ID = myArtId;
                ArtCheck.GetArtikeldatenByTableID();

                Arbeitsbereich = new clsArbeitsbereiche();
                Arbeitsbereich.InitCls(this.GLUser, ArtCheck.AbBereichID);

                FindInSameWorkspace = (ArtCheck.AbBereichID == this.Sys.AbBereich.ID);
                CreateInfoText();
            }
        }

        private void CreateInfoText()
        {
            InfoText = string.Empty;

            string strSql = "Select a.ID " +
                ", a.LVS_ID " +
                ", b.Name as Arbeitsbereich " +
                ", e.LEingangID " +
                ", aus.LAusgangID " +
                "FROM Artikel a " +
                    "INNER JOIN Arbeitsbereich b on b.ID=a.AB_ID " +
                    "INNER JOIN LEingang e on e.ID=a.LEingangTableID " +
                    "LEFT JOIN LAusgang aus on aus.ID = a.LAusgangTableID " +
                      "WHERE a.LVS_ID=" + (int)ArtCheck.LVS_ID +
                            " AND a.LEingangTableID>0";
            string strInfo = string.Empty;
            string strFirst = string.Empty;
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.GLUser.User_ID, "JumpTo");
            if (dt.Rows.Count > 0)
            {
                decimal decTmp = 0;
                foreach (DataRow row in dt.Rows)
                {
                    decTmp = 0;
                    if (decimal.TryParse(row["ID"].ToString(), out decTmp))
                    {
                        if (decTmp == ArtCheck.ID)
                        {
                            strFirst = "-> " + row["Arbeitsbereich"].ToString() + " >>> LVSNr.: " + String.Format("{000000:N0}", row["LVS_ID"].ToString()) +
                                                                                        " | E" + String.Format("{000000:N0}", row["LEingangID"].ToString());
                            if (row["LAusgangID"] != null)
                            {
                                decTmp = 0;
                                if ((decimal.TryParse(row["LAusgangID"].ToString(), out decTmp)) && (decTmp > 0))
                                {
                                    strFirst += " | A" + String.Format("{000000:N0}", row["LAusgangID"].ToString());
                                }
                            }
                        }
                        else
                        {
                            strInfo += "-> " + row["Arbeitsbereich"].ToString() + " >>> LVSNr.: " + String.Format("{000000:N0}", row["LVS_ID"].ToString()) +
                                                                                        " | E" + String.Format("{000000:N0}", row["LEingangID"].ToString());
                            if (row["LAusgangID"] != null)
                            {
                                decTmp = 0;
                                if ((decimal.TryParse(row["LAusgangID"].ToString(), out decTmp)) && (decTmp > 0))
                                {
                                    strInfo += " | A" + String.Format("{000000:N0}", row["LAusgangID"].ToString());
                                }
                            }
                        }
                    }
                }
                InfoText += Environment.NewLine + "Sie finden den Artikel in folgenden Arbeitsbereich:" + Environment.NewLine;
                InfoText += strFirst + Environment.NewLine;
                InfoText += strInfo;
            }
            else
            {
                InfoText += "LVSNr. ist in keinem Arbeitsbereich vorhanden!";
            }
            InfoText += Environment.NewLine;

            // wenn im nur ein Aritkel vorhanden, braucht es keine Info
            if ((dt.Rows.Count == 1) && FindInSameWorkspace)
            {
                InfoText = string.Empty;
            }

        }

    }
}
