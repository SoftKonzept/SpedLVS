using System;
using System.Collections.Generic;
using System.Data;
namespace LVS
{
    public class clsXmlStructure
    {
        public Globals._GL_SYSTEM GLSystem;
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
        public decimal XmlMesID { get; set; }
        public Int32 Knot { get; set; }
        public Int32 ParentKnot { get; set; }
        public Int32 Pos { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public string XmlSyntax { get; set; }
        public string XmlTxt { get; set; }
        public string Typ { get; set; }
        public Int32 Length { get; set; }
        public bool IsEM_Field { get; set; }
        public string EM_Value { get; set; }
        public bool IsAM_Field { get; set; }
        public string AM_Value { get; set; }
        public Dictionary<decimal, clsXmlStructure> dictEM { get; set; }
        public Dictionary<decimal, clsXmlStructure> dictAM { get; set; }

        private DataTable _dtXmlStruct;
        public DataTable dtXmlStruct
        {
            get
            {
                _dtXmlStruct = FillDataTableXmlStruct();
                return _dtXmlStruct;
            }
            set { _dtXmlStruct = value; }
        }

        /****************************************************************
         * 
         * *************************************************************/
        ///<summary>clsXmlMessages / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem)
        {
            this.GL_User = myGLUser;
            this.GLSystem = myGLSystem;
        }
        ///<summary>clsXmlMessages / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            /***
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
             * ***/
        }
        ///<summary>clsXmlMessages / Update</summary>
        ///<remarks></remarks>
        public void Update()
        {
            /****
            string strSql = string.Empty;
            strSql = "Update xmlMessages SET Bezeichnung='" + Bezeichnung + "'" +
                                                        ", Beschreibung='" + Beschreibung + "'" +

                                                        " WHERE ID=" + ID + ";";

            clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            this.Fill();
             * ****/
        }
        ///<summary>clsXmlMessages / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM xmlStructure WHERE ID=" + ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, BenutzerID, "xmlStruct");

            decimal decTmp = 0;
            Int32 iTmp = 0;

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                this.ID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["XmlMesID"].ToString(), out decTmp);
                this.XmlMesID = decTmp;
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["Knot"].ToString(), out iTmp);
                this.Knot = iTmp;
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["ParentKnot"].ToString(), out iTmp);
                this.ParentKnot = iTmp;
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["Pos"].ToString(), out iTmp);
                this.Pos = iTmp;
                this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                this.XmlSyntax = dt.Rows[i]["XmlSyntax"].ToString();
                this.XmlTxt = dt.Rows[i]["XmlTxt"].ToString();
                this.Typ = dt.Rows[i]["Typ"].ToString();
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["Length"].ToString(), out iTmp);
                this.Length = iTmp;

                this.IsEM_Field = (bool)dt.Rows[i]["EM_Field"];
                this.EM_Value = dt.Rows[i]["EM_Value"].ToString();
                this.IsAM_Field = (bool)dt.Rows[i]["AM_Field"];
                this.AM_Value = dt.Rows[i]["AM_Value"].ToString();
            }
        }
        ///<summary>clsXmlMessages / Fill</summary>
        ///<remarks></remarks>
        public DataTable FillDataTableXmlStruct()
        {
            dictAM = new Dictionary<decimal, clsXmlStructure>();
            dictEM = new Dictionary<decimal, clsXmlStructure>();

            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM xmlStructure WHERE xmlMesID=" + this.XmlMesID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, BenutzerID, "xmlStruct");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                string strXmlTxt = dt.Rows[i]["XmlTxt"].ToString();
                if (decTmp > 0)
                {
                    clsXmlStructure tmpXML = new clsXmlStructure();
                    tmpXML.GL_User = this.GL_User;
                    tmpXML.GLSystem = this.GLSystem;
                    tmpXML.ID = decTmp;
                    tmpXML.Fill();

                    if ((bool)dt.Rows[i]["EM_Field"])
                    {
                        dictEM.Add(decTmp, tmpXML);
                    }
                    if ((bool)dt.Rows[i]["AM_Field"])
                    {
                        dictAM.Add(decTmp, tmpXML);
                    }
                }
            }
            return dt;
        }
    }
}
