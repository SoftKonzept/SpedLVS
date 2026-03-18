using System;
using System.Data;
using System.Net;
using System.Xml.Linq;

namespace LVS
{
    public class clsDistance
    {

        const string gMapsLink = @"http://maps.google.com/maps/api/directions/xml?origin={0}&destination={1}&sensor=false";
        //const string gStaticMapsLink ="http://maps.google.com/maps/api/staticmap?size=600x600&sensor=false&path=weight:4|color:blue|enc:";
        const string gStaticMapsLink = "http://maps.google.com/maps/api/staticmap?size=600x600&markers=color:green%7Clabel:S%7C{0}&markers=color:white%7Clabel:Z%7C{1}&sensor=false&path=weight:4|color:blue|enc:";

        public Globals._GL_USER GL_User;
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
        //************************************
        public decimal ID { get; set; }
        public Int32 km { get; set; }
        public Int32 kmGMaps { get; set; }
        public string vPLZ { get; set; }
        public string vOrt { get; set; }
        public string nPLZ { get; set; }
        public string nOrt { get; set; }
        public bool kmExist { get; set; }
        public string nLand { get; set; }
        public string vLand { get; set; }
        public bool IsgMaps { get; set; }
        private string _gMapStartADR;
        public string gMapStartADR
        {
            get
            {
                _gMapStartADR = vPLZ + " " + vOrt + " " + vLand;
                return _gMapStartADR;
            }
            set { _gMapStartADR = value; }
        }
        private string _gMapEndADR;
        public string gMapEndADR
        {
            get
            {
                _gMapEndADR = nPLZ + " " + nOrt + " " + nLand;
                return _gMapEndADR;
            }
            set { _gMapEndADR = value; }
        }

        public string overview_polyline { get; set; }
        private Uri _gMapsBrowserLink;
        public Uri gMapsBrowserLink
        {
            get
            {
                string strTmp = String.Format(gStaticMapsLink, gMapStartADR, gMapEndADR);
                Uri tmpUri = new Uri(strTmp + overview_polyline);
                _gMapsBrowserLink = tmpUri;
                return _gMapsBrowserLink;
            }
            set { _gMapsBrowserLink = value; }
        }

        /********************************************************************************
         *                        Methoden / Procedure
         * ******************************************************************************/
        ///<summary>clsDistance / FillByAdrID</summary>
        ///<remarks></remarks>
        public void FillByAdrID(decimal decVonADR, decimal decNachADR)
        {
            //von Ort
            DataTable dt = clsADR.GetADRbyID(decVonADR, this.GL_User.User_ID);
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    vPLZ = dt.Rows[i]["PLZ"].ToString();
                    vOrt = dt.Rows[i]["Ort"].ToString();
                    vLand = dt.Rows[i]["Land"].ToString();
                }
            }
            //NachOrt
            dt.Clear();
            dt = clsADR.GetADRbyID(decNachADR, this.GL_User.User_ID);
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    nPLZ = dt.Rows[i]["PLZ"].ToString();
                    nOrt = dt.Rows[i]["Ort"].ToString();
                    nLand = dt.Rows[i]["Land"].ToString();
                }
            }

            //Ermittel ob es Eintragungen für diese Strecke gibt
            ExistDistance();
            if (kmExist)
            {
                FillByID();
            }
            else
            {
                //KM Ermittelung über Google
                GetGMapDistance();
                AddDistance();
                IsgMaps = true;
            }
        }
        ///<summary>clsDistance / FillByID</summary>
        ///<remarks></remarks>
        public void FillByID()
        {
            if (ExistDistanceID())
            {
                string strSQL = string.Empty;
                strSQL = "Select * FROM Distance WHERE ID=" + ID + "; ";
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, GL_User.User_ID, "Distance");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        ID = (decimal)dt.Rows[i]["ID"];
                        vPLZ = dt.Rows[i]["vPLZ"].ToString();
                        vOrt = dt.Rows[i]["vOrt"].ToString();
                        vLand = dt.Rows[i]["vLand"].ToString();
                        nPLZ = dt.Rows[i]["nPLZ"].ToString();
                        nOrt = dt.Rows[i]["nOrt"].ToString();
                        nLand = dt.Rows[i]["nLand"].ToString();
                        km = (Int32)dt.Rows[i]["km"];
                        IsgMaps = (bool)dt.Rows[i]["gMaps"];
                        if (IsgMaps)
                        {
                            kmGMaps = km;
                        }
                        else
                        {
                            kmGMaps = 0;
                        }
                    }
                }
            }
        }
        ///<summary>clsDistance / GetGMapDistance</summary>
        ///<remarks></remarks>
        public void GetGMapDistance()
        {
            //Check Adressen müssen korrekt sein
            if ((gMapStartADR != string.Empty) && (gMapEndADR != string.Empty))
            {
                try
                {
                    string strMaps = String.Format(gMapsLink, gMapStartADR, gMapEndADR);
                    XDocument doc = XDocument.Load(String.Format(gMapsLink, gMapStartADR, gMapEndADR));
                    XElement dirResp = doc.Element(XName.Get("DirectionsResponse"));
                    XElement eStatus = dirResp.Element(XName.Get("status"));
                    if (eStatus.Value == "OK")
                    {
                        XElement eRoute = dirResp.Element(XName.Get("route"));
                        XElement eLeg = eRoute.Element(XName.Get("leg"));
                        XElement eDuration = eLeg.Element(XName.Get("duration"));
                        XElement eDistance = eLeg.Element(XName.Get("distance"));
                        XElement eStartAddress = eLeg.Element(XName.Get("start_address"));
                        XElement eEndAddress = eLeg.Element(XName.Get("end_address"));
                        XElement eOverview_Polyline = eRoute.Element(XName.Get("overview_polyline"));
                        //XElement ePoints = eRoute.Element(XName.Get("points"));
                        // route.Duration = Convert.ToUInt64(eDuration.Element(XName.Get("value")).Value).ToString();
                        overview_polyline = (eOverview_Polyline.Element(XName.Get("points")).Value).ToString();
                        string strTmp = Convert.ToUInt64(eDistance.Element(XName.Get("value")).Value).ToString();
                        Int32 iTmp = 0;
                        Int32.TryParse(strTmp, out iTmp);
                        if (iTmp > 0)
                        {
                            iTmp = iTmp / 1000;
                        }
                        kmGMaps = iTmp;
                        km = kmGMaps;
                        IsgMaps = true;
                    }
                    //Baustelle XML-Datei speichern
                    //doc.Save("D:Route.xml", SaveOptions.None);
                }
                // Ein Verbindungsfehler wurde abgefangen  
                catch (WebException ex)
                {
                    //route.Status = RouteStatus.ConnectionError;
                }
                // Ein unbekannter Fehler ist aufgetreten  
                catch (Exception ex)
                {
                    //route.Status = RouteStatus.UnknownError;
                }
            }
        }
        ///<summary>clsDistance / GetDistance</summary>
        ///<remarks></remarks>
        public void GetDistance(string PLZ_Start, string Ort_Start, string PLZ_Ziel, string Ort_Ziel)
        {
            if ((PLZ_Start != string.Empty) &&
                (Ort_Start != string.Empty) &&
                (PLZ_Ziel != string.Empty) &&
                (Ort_Ziel != string.Empty))
            {
                vOrt = Ort_Start;
                vPLZ = PLZ_Start;
                nPLZ = PLZ_Ziel;
                nOrt = Ort_Ziel;

                //ExistDistance();
                if (kmExist)
                {
                    GetDistanceKm();
                }
                else
                {
                    SetkmToZero();
                }
            }
            else
            {
                SetkmToZero();
            }
        }
        ///<summary>clsDistance / GetDistance</summary>
        ///<remarks>Prüft,ob die km für die Entfernung bereits vorhanden ist</remarks>
        public void ExistDistance()
        {
            string strSQL = string.Empty;
            strSQL = "SELECT ID From Distance WHERE vPLZ= '" + vPLZ + "' AND " +
                                                    "vOrt='" + vOrt + "' AND " +
                                                    "vLand='" + vLand + "' AND " +
                                                    "nPLZ='" + nPLZ + "' AND " +
                                                    "nOrt='" + nOrt + "' AND " +
                                                    "nLand='" + nLand + "' ; ";
            kmExist = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, this.GL_User.User_ID);

            if (kmExist)
            {
                decimal decTmp = 0;
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, this.GL_User.User_ID);
                decimal.TryParse(strTmp, out decTmp);
                ID = decTmp;
            }
        }
        ///<summary>clsDistance / GetDistance</summary>
        ///<remarks>Entfernung zwischen zwei ADR</remarks>
        private void GetDistanceKm()
        {
            string strSQL = string.Empty;
            strSQL = "SELECT km From Distance WHERE ID='" + ID + "'";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            km = iTmp;
        }
        ///<summary>clsDistance / SetkmToZero</summary>
        ///<remarks></remarks>
        private void SetkmToZero()
        {
            km = 0;
        }
        ///<summary>clsDistance / SetkmToZero</summary>
        ///<remarks>Insert Entfernung </remarks>
        public void AddDistance()
        {
            string strSQL = "INSERT INTO Distance (vPLZ, " +
                                                  "vOrt, " +
                                                  "nPLZ, " +
                                                  "nOrt, " +
                                                  "km, " +
                                                  "vLand, " +
                                                  "nLand, " +
                                                  "gMaps) " +

                                                  "VALUES ('" + vPLZ + "','"
                                                               + vOrt + "','"
                                                               + nPLZ + "','"
                                                               + nOrt + "','"
                                                               + km + "','"
                                                               + vLand + "','"
                                                               + nLand + "','"
                                                               + Convert.ToInt32(IsgMaps) + "') ; ";
            strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
            }
        }
        ///<summary>clsDistance / UpdateDistanceKM</summary>
        ///<remarks>update km - Entfernung</remarks>
        public void UpdateDistanceKM()
        {
            string stSQL = "Update Distance SET km='" + km + "' " +
                                            "WHERE ID='" + ID + "'";
            clsSQLcon.ExecuteSQL(stSQL, GL_User.User_ID);
        }
        ///<summary>clsDistance / GetAllDistance</summary>
        ///<remarks>/remarks>
        public static DataTable GetAllDistance(decimal decBenutzer)
        {
            string sql = "Select * From Distance Order By vPLZ";

            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(sql, decBenutzer, "Dinstance");
            return dt;
        }
        ///<summary>clsDistance / UpdateDistance</summary>
        ///<remarks>/remarks>
        public void UpdateDistance()
        {
            if (ID > 0)
            {
                string strSQL = "Update Distance SET " +
                                                        "vPLZ='" + vPLZ + "', " +
                                                        "vOrt='" + vOrt + "', " +
                                                        "vLand='" + vLand + "', " +
                                                        "nPLZ='" + nPLZ + "', " +
                                                        "nOrt='" + nOrt + "', " +
                                                        "nLand='" + nLand + "', " +
                                                        "gMaps='" + Convert.ToInt32(IsgMaps) + "', " +
                                                        "km='" + km + "' " +
                                                        "WHERE ID='" + ID + "'";
                clsSQLcon.ExecuteSQL(strSQL, GL_User.User_ID);
            }
            else
            {
                string strSQL = "Update Distance SET " +
                                            "km='" + km + "' " +
                                            "WHERE " +
                                            "vPLZ='" + vPLZ + "' AND " +
                                            "vOrt='" + vOrt + "' AND" +
                                            "vLand='" + nLand + "' AND " +
                                            "nPLZ='" + nPLZ + "' AND " +
                                            "nOrt='" + nOrt + "' AND " +
                                            "nLand='" + nLand + "' AND " +
                                            "gMaps=" + Convert.ToInt32(IsgMaps) + ";";

                clsSQLcon.ExecuteSQL(strSQL, GL_User.User_ID);
            }
        }
        ///<summary>clsDistance / UpdateDistance</summary>
        ///<remarks>löscht einen Distance nach ID</remarks>
        public void DeleteDistanceByID()
        {
            if (ExistDistanceID())
            {
                string strSQL = "Delete Distance WHERE ID=" + ID + ";";
                clsSQLcon.ExecuteSQL(strSQL, GL_User.User_ID);
            }
        }
        ///<summary>clsDistance / DeleteDistanceZero</summary>
        ///<remarks>löscht alle Einträge mit der km =0 </remarks>
        public void DeleteDistanceZero()
        {
            string strSQL = "Delete Distance WHERE km=0 ;";
            clsSQLcon.ExecuteSQL(strSQL, GL_User.User_ID);
        }
        ///<summary>clsDistance / ExistDistanceID</summary>
        ///<remarks></remarks>
        private bool ExistDistanceID()
        {
            string strSQL = "Select ID FROM Distance WHERE ID=" + ID + ";";
            bool bTmp = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, GL_User.User_ID);
            return bTmp;
        }
    }
}
