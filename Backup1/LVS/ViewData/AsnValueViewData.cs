using LVS.Models;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class AsnValueViewData
    {
        public Asn AsnHead { get; set; }
        public AsnValues AsnValue { get; set; }
        public List<AsnValues> ListAsnValues { get; set; } = new List<AsnValues>();

        public AsnValueViewData()
        {
            InitCls();
        }
        public DataTable dtAsnValues = new DataTable();
        public AsnValueViewData(Asn myAsnHead) : this()
        {
            AsnHead = myAsnHead;
            if ((AsnHead is Asn) && (AsnHead.Id > 0))
            {
                FillByHeadAsnId();
            }
        }
        public AsnValueViewData(AsnValues myAsnValue) : this()
        {
            AsnValue = myAsnValue;
            if ((AsnValue is AsnValues) && (AsnValue.Id > 0))
            {
                FillByAsnValueId();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            //system = new clsSystem();
            AsnHead = new Asn();
            AsnValue = new AsnValues();
            ListAsnValues = new List<AsnValues>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void FillByAsnValueId()
        {
            string strSQL = sql_GetListByAsnValueId;
            dtAsnValues = new DataTable();
            dtAsnValues = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, 1, "AsnValue");
            if (dtAsnValues.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAsnValues.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void FillByHeadAsnId()
        {
            string strSQL = sql_GetListByHeadAsnId;
            dtAsnValues = new DataTable();
            dtAsnValues = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, 1, "AsnValue");
            if (dtAsnValues.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAsnValues.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public bool DeleteAllByAsnId(Asn myAsn)
        {
            this.AsnHead = myAsn;
            string strSQL = sql_DeleteAllByAsnId;
            return clsSQLCOM.ExecuteSQLWithTRANSACTION(strSQL, "DeleteAsnValue", 1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public void SetValue(DataRow row)
        {
            AsnValue = new AsnValues();

            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            AsnValue.Id = iTmp;

            iTmp = 0;
            int.TryParse(row["ASNID"].ToString(), out iTmp);
            AsnValue.AsnId = iTmp;

            iTmp = 0;
            int.TryParse(row["ASNFieldID"].ToString(), out iTmp);
            AsnValue.AsnFieldId = iTmp;

            AsnValue.FieldName = row["FieldName"].ToString();
            AsnValue.Value = row["FieldName"].ToString();
            AsnValue.ASNFileTyp = row["ASNFileTyp"].ToString();
            AsnValue.Typ = row["Kennung"].ToString();
            AsnValue.SatzKennung = row["SatzKennung"].ToString();

        }

        ///-----------------------------------------------------------------------------------------------------
        ///                             sql Statements
        ///-----------------------------------------------------------------------------------------------------

        /// <summary>
        ///             Add sql - String
        /// </summary>
        public string sql_Add
        {
            get
            {
                string strSQL = string.Empty;

                return strSQL;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_GetListByAsnValueId
        {
            get
            {
                string strSql = string.Empty;
                strSql = this.sql_Get_Main + " WHERE ASNValue.ID =" + AsnValue.Id;

                return strSql;
            }
        }

        /// <summary>
        ///             GET List
        /// </summary>
        public string sql_GetListByHeadAsnId
        {
            get
            {
                string strSql = string.Empty;
                strSql = this.sql_Get_Main + " WHERE ASNValue.ASNID =" + AsnHead.Id;
                strSql += " Order By ASNValue.ID";
                return strSql;
            }
        }
        /// <summary>
        ///             GET_Main
        /// </summary>
        public string sql_Get_Main
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select " +
                            "ASNValue.* " +
                            ",ASN.ASNFileTyp " +
                            ", ASNTyp.Typ as Typ " +
                            ", ASNArtSatzFeld.Kennung" +
                            ", ASNArtSatz.Kennung as SatzKennung" +
                            " FROM ASNValue " +
                            "INNER JOIN ASN ON ASN.ID=ASNValue.ASNID " +
                            "INNER JOIN ASNTyp ON ASNTyp.ID=ASN.ASNTypID " +
                            "INNER JOIN ASNArtSatzFeld ON ASNArtSatzFeld.ID = ASNValue.ASNFieldID " +
                            "INNER JOIN ASNArtSatz ON ASNArtSatz.ID =ASNArtSatzFeld.ASNSatzID ";
                return strSql;
            }
        }

        /// <summary>
        ///             DELETE sql - String
        /// </summary>
        public string sql_Delete
        {
            get
            {
                string strSql = string.Empty;
                strSql = "DELETE FROM ASNValue WHERE ID=" + AsnValue.Id;
                return strSql;
            }
        }

        public string sql_DeleteAllByAsnId
        {
            get
            {
                string strSql = string.Empty;
                strSql = "DELETE FROM ASNValue WHERE ASNID=" + AsnHead.Id;
                return strSql;
            }
        }
        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update
        {
            get
            {
                string strSql = string.Empty;
                return strSql;
            }
        }


    }
}

