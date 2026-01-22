using System;
using System.Data;

namespace LVS.Tmp
{
    class clsTmpVerwTWB
    {

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

        public decimal ID { get; set; }
        public Int32 SAP { get; set; }
        public decimal Dicke { get; set; }
        public decimal Breite { get; set; }
        public decimal Laenge { get; set; }
        public string Guete { get; set; }
        public bool aktiv { get; set; }
        public string VertragNr { get; set; }
        public decimal GArtID { get; set; }


        ///<summary>clsTmpVerwTWB / GetASNValueDictByASNId</summary>
        ///<remarks></remarks>
        public void FillBySAP()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT TOP(1) * FROM TmpVerwTWB WHERE SAP=" + SAP + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ASNValue");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                this.ID = decTmp;
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["SAP"].ToString(), out iTmp);
                this.SAP = iTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Dicke"].ToString(), out decTmp);
                this.Dicke = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Breite"].ToString(), out decTmp);
                this.Breite = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Laenge"].ToString(), out decTmp);
                this.Laenge = decTmp;
                this.Guete = dt.Rows[i]["Güte"].ToString();
                this.aktiv = (bool)dt.Rows[i]["Status"];

                //Warengruppe ermitteln
                GetWarengruppenID();
                this.GArtID = clsLagerdaten.GetWarengruppenID(this.GL_User, this.Dicke, this.Breite, this.Laenge);
            }
        }
        ///<summary>clsTmpVerwTWB / GetWarengruppenID</summary>
        ///<remarks>Ermittlung der Warengruppe über die Abmessung:
        ///         - alle drei Abmessungen = Pakete
        ///         - 2 Abmessungen = Coils
        ///         </remarks>
        private void GetWarengruppenID()
        {
            string strSql = string.Empty;
            if (
                   (this.Dicke > 0) &
                   (this.Breite > 0) &
                   (this.Laenge > 0)
              )
            {
                strSql = "Select Top(1) ID FROM Gueterart WHERE Bezeichnung='Pakete' OR Bezeichnung='PAKETE';";
            }
            else
            {
                strSql = "Select Top(1) ID FROM Gueterart WHERE Bezeichnung='COILS' OR Bezeichnung='Coils';";
            }
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            this.GArtID = decTmp;
        }
    }
}
