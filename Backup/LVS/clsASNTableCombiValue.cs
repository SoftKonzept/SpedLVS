using System;
using System.Collections.Generic;
using System.Data;


namespace LVS
{
    public class clsASNTableCombiValue
    {
        public const string const_PlaceHolder = "#";
        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GL_System;
        //************  User  ***************
        private decimal _BenutzerID;

        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set
            {
                _BenutzerID = value;
            }
        }

        //************************************
        public decimal ID { get; set; }
        public decimal Sender { get; set; }
        public decimal AbBereichID { get; set; }
        public decimal Receiver { get; set; }
        public string TableName { get; set; }
        public string ColValue { get; set; }
        public string ColsForCombination { get; set; }

        public bool UseValueSeparator { get; set; }
        public string ValueSeparator { get; set; } = string.Empty; // Default value separator

        public List<string> ListColsForCombination = new List<string>();




        /******************************************************************************
         *                          Methoden / Procedure
         * ***************************************************************************/
        ///<summary>clsASNTableCombiValue / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSystem;
        }
        ///<summary>clsASNTableCombiValue / Copy</summary>
        ///<remarks></remarks>
        public clsASNTableCombiValue Copy()
        {
            return (clsASNTableCombiValue)this.MemberwiseClone();
        }
        ///<summary>clsASNTableCombiValue / Fill</summary>
        ///<remarks></remarks>>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ASNTableCombiValue WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "TableCombiVal");
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                this.ID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AbBereichID"].ToString(), out decTmp);
                this.AbBereichID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Sender"].ToString(), out decTmp);
                this.Sender = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Receiver"].ToString(), out decTmp);
                this.Receiver = decTmp;
                this.TableName = dt.Rows[i]["TableName"].ToString();
                this.ColValue = dt.Rows[i]["ColValue"].ToString();
                this.ColsForCombination = dt.Rows[i]["ColsForCombination"].ToString();
                this.UseValueSeparator = Convert.ToBoolean(dt.Rows[i]["UseValueSeparator"]);
                this.ValueSeparator = dt.Rows[i]["ValueSeparator"].ToString();

                //List mit ColForCombination
                FillListColsForCombination();
            }
        }
        ///<summary>clsASNTableCombiValue / FillListColsForCombination</summary>
        ///<remarks>extrahiert die einzelnen Spalten aus dem String</remarks>>
        private void FillListColsForCombination()
        {
            this.ListColsForCombination = new List<string>();
            string ColString = this.ColsForCombination;
            while (ColString.Length > 0)
            {
                string strTmp = ColString;
                string strCol = string.Empty;
                Int32 iStringLength = strTmp.Length;
                Int32 iPosPlaceHolder = 0;
                iPosPlaceHolder = strTmp.IndexOf(clsASNTableCombiValue.const_PlaceHolder);
                if (iPosPlaceHolder > 0)
                {
                    iPosPlaceHolder = strTmp.IndexOf(clsASNTableCombiValue.const_PlaceHolder);
                    strCol = strTmp.Substring(0, iPosPlaceHolder);
                }
                else
                {
                    strCol = strTmp;
                }
                this.ListColsForCombination.Add(strCol);
                iPosPlaceHolder++;
                try
                {
                    if (strCol != strTmp)
                    {
                        ColString = strTmp.Substring(iPosPlaceHolder, iStringLength - iPosPlaceHolder);
                    }
                    else
                    {
                        ColString = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    ColString = string.Empty;
                }
            }
        }
        ///<summary>clsASNTableCombiValue / GetArtikelFieldAssignment</summary>
        ///<remarks></remarks>>
        public Dictionary<string, clsASNTableCombiValue> GetArtikelFieldAssignment(decimal mySender, decimal myReceiver)
        {
            Dictionary<string, clsASNTableCombiValue> retDict = new Dictionary<string, clsASNTableCombiValue>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ASNTableCombiValue WHERE Sender=" + mySender + " AND Receiver=" + myReceiver + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ASNTableCombiValue");
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsASNTableCombiValue tmp = new clsASNTableCombiValue();
                    tmp._GL_User = this._GL_User;
                    tmp.ID = decTmp;
                    tmp.Fill();
                    retDict.Add(tmp.ColValue, tmp);
                }
            }
            return retDict;
        }


    }
}
