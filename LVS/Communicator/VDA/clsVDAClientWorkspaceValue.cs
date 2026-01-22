using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using LVS;

namespace LVS
{
    class clsVDAClientWorkspaceValue
    {

        public clsSQLCOM SQLConIntern = new clsSQLCOM();

        //internal clsASNArt ASNArt;
        //internal clsASNValue ASNValue;
        //internal clsASNTyp ASNTyp;
        //public DataTable dtVDAClientValue;

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
        public decimal AdrID { get; set; }
        public decimal Receiver { get; set; }
        public decimal AbBereichID { get; set; }
        public decimal ASNFieldID { get; set; }
        public string Kennung { get; set; }
        public string Value { get; set; }
        public bool aktiv { get; set; }
        public bool IsFunction { get; set; }

        public Dictionary<decimal, clsVDAClientWorkspaceValue> DictVDAClientWorkspaceValue { get; set; }


        /**********************************************************************************
         *                      Methoden / Procedure
         * *******************************************************************************/
        ///<summary>clsVDAClientConstValue / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, decimal myAdrID, decimal myReceiver, decimal myAbBereichID)
        {
            this.GL_User = myGLUser;
            this.AdrID = myAdrID;
            this.Receiver = myReceiver;
            this.AbBereichID = myAbBereichID;
            InitDictVDAClientValue();
        }
        ///<summary>clsVDAClientConstValue / InitDictVDAClientValue</summary>
        ///<remarks></remarks>
        public void InitDictVDAClientValue()
        {
            DictVDAClientWorkspaceValue = new Dictionary<decimal, clsVDAClientWorkspaceValue>();

            DataTable dt = new DataTable("VDAClientConstValue");
            string strSQL = string.Empty;
            strSQL = "SELECT a.* " +
                                "FROM VDAClientWorkspaceValue a " +
                                "WHERE "+
                                    "a.AdrID=" +(Int32) this.AdrID +
                                    " AND a.Receiver =" + (Int32)this.Receiver +
                                    " AND a.AbBereichID=" + (Int32)this.AbBereichID +
                                    " AND a.activ=1 "+
                                    " Order by a.ASNFieldID "+
                                    ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClientConstValue");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                clsVDAClientWorkspaceValue tmp = new clsVDAClientWorkspaceValue();
                tmp.GL_User = this.GL_User;
                tmp.ID = (decimal)dt.Rows[i]["ID"];
                tmp.Fill();
                if (!DictVDAClientWorkspaceValue.ContainsKey(tmp.ASNFieldID))
                {
                    DictVDAClientWorkspaceValue.Add(tmp.ASNFieldID, tmp);
                }
            }
        }
        ///<summary>clsVDAClientConstValue / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable("VDAClientValue");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM VDAClientWorkspaceValue WHERE ID=" + this.ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClientValue");
            FillClassByTable(dt);
        }
        ///<summary>clsVDAClientConstValue / FillClassByTable</summary>
        ///<remarks></remarks>
        private void FillClassByTable(DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)            
            {
                decimal decTmp = 0;
                this.ID = (decimal)dt.Rows[i]["ID"];
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AdrID"].ToString(), out decTmp);
                this.AdrID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Receiver"].ToString(), out decTmp);
                this.Receiver = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AbBereichID"].ToString(), out decTmp);
                this.AbBereichID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ASNFieldID"].ToString(), out decTmp);
                this.ASNFieldID = decTmp;
                this.Value = dt.Rows[i]["Value"].ToString();
                this.aktiv = (bool)dt.Rows[i]["activ"];
                this.IsFunction = (bool)dt.Rows[i]["IsFunction"];
            }
        }


    }
}
