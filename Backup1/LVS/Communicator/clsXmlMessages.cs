using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
namespace LVS
{
    public class clsXmlMessages
    {

        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GL_User;

        internal clsXmlStructure XmlStruct;
        internal clsJobs Job;
        internal clsASN ASN;
        internal clsASNTyp ASNTyp;

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
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }

        /***************************************************************************
         *                  Methoden / Process
         * *************************************************************************/
        ///<summary>clsXmlMessages / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_SYSTEM myGLSys, Globals._GL_USER myGLUser)
        {
            this.GL_User = myGLUser;
            this.GLSystem = myGLSys;

            XmlStruct = new clsXmlStructure();
            XmlStruct.InitClass(this.GL_User, this.GLSystem);

            ASN = new clsASN();
            ASN.InitClass(this.GLSystem, this.GL_User);

            ASNTyp = new clsASNTyp();
            ASNTyp.InitClass(ref this.GL_User);
            ASNTyp.FillDictASNTyp();
        }
        ///<summary>clsXmlMessages / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO xmlMessages (Bezeichnung, Beschreibung) " +
                                            "VALUES ('" + Bezeichnung + "'" +
                                                    ", " + Beschreibung +
                                                    ");";
            strSql = strSql + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);

            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
                Fill();
            }
        }
        ///<summary>clsXmlMessages / Update</summary>
        ///<remarks></remarks>
        public void Update()
        {
            string strSql = string.Empty;
            strSql = "Update xmlMessages SET Bezeichnung='" + Bezeichnung + "'" +
                                                        ", Beschreibung='" + Beschreibung + "'" +

                                                        " WHERE ID=" + ID + ";";

            clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            this.Fill();
        }
        ///<summary>clsXmlMessages / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM xmlMessages WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Einheit");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.Bezeichnung = dt.Rows[i]["ViewID"].ToString();
                this.Beschreibung = dt.Rows[i]["KD_ID"].ToString();

                //this.XmlStruct = new clsXmlStructure();
                //this.XmlStruct.XmlMesID = this.ID;
                //this.XmlStruct.FillDataTableXmlStruct();
            }
        }
        ///<summary>clsXmlMessages / Read</summary>
        ///<remarks></remarks>
        public List<clsASNValue> Read(string myXmlPath)
        {
            List<clsASNValue> listInsert = new List<clsASNValue>();
            if (Job != null)
            {
                this.XmlStruct.XmlMesID = Job.ASNArtID;
                DataTable dtStruct = this.XmlStruct.dtXmlStruct;
                XmlTextReader reader = new XmlTextReader(myXmlPath);
                clsASNValue TmpASNVal = new clsASNValue();
                Int32 Count = 0;
                string strName = string.Empty;
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            TmpASNVal = new clsASNValue();
                            TmpASNVal.FieldName = reader.Name;
                            switch (TmpASNVal.FieldName)
                            {
                                case "UNIPORT":
                                case "HEADER":
                                case "TRANSPORT":
                                case "TRAN_PART":
                                case "LS_PART":
                                case "LS":
                                case "LSPOS":
                                case "CHRG":
                                    TmpASNVal.Value = string.Empty;
                                    TmpASNVal.ASNID = 0;
                                    TmpASNVal.ASNFieldID = 0;
                                    listInsert.Add(TmpASNVal);
                                    break;
                            }
                            break;

                        case XmlNodeType.Text: //Display the text in each element.

                            clsASNValue Tmp1 = new clsASNValue();
                            Tmp1.FieldName = TmpASNVal.FieldName;
                            Tmp1.ASNID = 0;
                            Tmp1.ASNFieldID = 0;
                            Tmp1.Value = reader.Value;

                            Int32 iLen = 0;
                            for (Int32 i = 0; i < dtStruct.Rows.Count - 1; i++)
                            {
                                string strKnoten = dtStruct.Rows[i]["XmlTxt"].ToString();
                                //Meldungsart setzen wie Lfs, AbE, AbA usw.
                                switch (strKnoten)
                                {
                                    case "TASK":
                                        ASN.ASNTypID = GetXMLASNTyp(Tmp1.Value.ToString());
                                        break;
                                }

                                if (strKnoten == Tmp1.FieldName)
                                {
                                    Int32.TryParse(dtStruct.Rows[i]["Length"].ToString(), out iLen);
                                    break;
                                }
                            }
                            if (Tmp1.Value.ToString().Length > iLen)
                            {
                                Tmp1.Value = Tmp1.Value.Substring(0, iLen);
                            }
                            listInsert.Add(Tmp1);
                            Count++;
                            break;
                    }
                }
                reader.Close();
            }
            return listInsert;
        }
        ///<summary>clsXmlMessages / GetXMLASNTyp</summary>
        ///<remarks></remarks>
        private decimal GetXMLASNTyp(string strValue)
        {
            Int32 decReVal = 0;
            switch (strValue)
            {
                //Lfs vom Auftraggeber
                case "AVIS":
                    ASNTyp.dictASNTyp.TryGetValue("LSL", out decReVal);
                    break;
                //Abruf Auftraggeber
                case "DISP":
                    ASNTyp.dictASNTyp.TryGetValue("AbA", out decReVal);
                    break;
                default:
                    decReVal = 0;
                    break;
            }
            return (decimal)decReVal;
        }
    }
}
