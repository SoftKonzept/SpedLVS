//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data;
//using LVS;

//namespace LVS
//{
//   public class clsASNTyp
//    {
//        public Globals._GL_USER GL_User;
//        //************  User  ***************
//        private decimal _BenutzerID;
//        public decimal BenutzerID
//        {
//            get
//            {
//                _BenutzerID = GL_User.User_ID;
//                return _BenutzerID;
//            }
//            set { _BenutzerID = value; }
//        }

//        public const decimal const_ASNTyp_LSL = 1;  // erst mal so später über db

//        public decimal ID { get; set; }
//        public string Typ { get; set; }
//        public string Beschreibung { get; set; }
//        public Dictionary<decimal, clsASNTyp> dictASNTypCls;
//        public Dictionary<string, decimal> dictASNTyp;


//        /**********************************************************************
//         *                  Methoden / Procedure
//         * *******************************************************************/
//        ///<summary>clsASNTyp / InitClass</summary>
//        ///<remarks></remarks>
//        public void InitClass(ref Globals._GL_USER myGLUser)
//        {
//            this.GL_User = myGLUser;
//        }
//        ///<summary>clsASNTyp / FillDictASNTyp</summary>
//        ///<remarks></remarks>
//        public void FillDictASNTyp()
//        {
//            dictASNTypCls = new Dictionary<decimal, clsASNTyp>();
//            dictASNTyp = new Dictionary<string, decimal>();

//            DataTable dt = new DataTable("ASNTyp");
//            string strSQL = string.Empty;
//            strSQL = "SELECT * FROM ASNTyp;";
//            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, this.BenutzerID, "ASNTyp");
//            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
//            {
//                decimal decTmp=0;
//                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
//                if(decTmp>0)
//                {
//                    clsASNTyp tmpTyp = new clsASNTyp();
//                    tmpTyp.GL_User = this.GL_User;
//                    tmpTyp.ID = decTmp;
//                    tmpTyp.Fill();

//                    this.dictASNTypCls.Add(decTmp, tmpTyp);
//                    this.dictASNTyp.Add(tmpTyp.Typ.ToString(), decTmp);
//                }
//            }            
//        }
//        ///<summary>clsASNTyp / Fill</summary>
//        ///<remarks></remarks>
//        public void Fill()
//        {
//            DataTable dt = new DataTable("ASNTyp");
//            string strSQL = string.Empty;
//            strSQL = "SELECT * FROM ASNTyp WHERE ID=" + ID + ";";
//            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNTyp");
//            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
//            {
//                this.ID = (decimal)dt.Rows[i]["ID"];
//                this.Typ = dt.Rows[i]["Typ"].ToString();
//                this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
//            }
//        }
//        /*************************************************************************
//          *                      public static
//          * **********************************************************************/
//        public static Dictionary<decimal, string> GetASNTypDict(decimal myBenutzer)
//        {
//           Dictionary<decimal, string> dict = new Dictionary<decimal, string>();
//           DataTable dt = new DataTable("ASNTyp");
//           string strSQL = string.Empty;
//           strSQL = "SELECT * FROM ASNTyp Order BY ID";
//           dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, myBenutzer, "ASNTyp");
//           for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
//           {
//              decimal decID = (decimal)dt.Rows[i]["ID"];
//              string strTyp = dt.Rows[i]["Typ"].ToString();
//              dict.Add(decID, strTyp);
//           }
//           return dict;
//        }
//    }
//}
