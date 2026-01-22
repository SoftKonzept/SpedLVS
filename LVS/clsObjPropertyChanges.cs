using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsObjPropertyChanges
    {
        public const string TableName_Artikel = "Artikel";
        public const string TableName_LAusgang = "LAusgang";
        public const string TableName_LEingang = "LEingang";

        public Globals._GL_USER _GL_User;

        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get { return _BenutzerID; }
            set { _BenutzerID = value; }
        }
        //************************************

        public int ID { get; set; }
        public int TableId { get; set; }
        public string TableName { get; set; }
        public string Property { get; set; }
        public string ValueOld { get; set; }
        public string ValueNew { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public List<string> ListTableNames
        {
            get
            {
                List<string> list = new List<string>();
                list.Add(clsObjPropertyChanges.TableName_Artikel);
                list.Add(clsObjPropertyChanges.TableName_LAusgang);
                list.Add(clsObjPropertyChanges.TableName_LEingang);
                return list;
            }
        }



        public void InitCls(Globals._GL_USER myGLuser)
        {
            this._GL_User = myGLuser;
        }
        /// <summary>
        /// 
        /// </summary>
        public string AddSql()
        {
            string strSQL = " INSERT INTO ObjPropertyChanges ([TableId],[TableName],[Property],[ValueOld] " +
                                                            ",[ValueNew] ,[UserId], [CreateDate]) " +
                            " VALUES (" +
                                        this.TableId +
                                        ",'" + this.TableName + "'" +
                                        ",'" + this.Property + "'" +
                                        ",'" + this.ValueOld + "'" +
                                        ",'" + this.ValueNew + "'" +
                                        "," + this.UserId +
                                        ",'" + CreateDate + "'" + //GETDATE() in Table
                                     "); ";
            return strSQL;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Add()
        {
            try
            {
                string strSQL = AddSql();
                strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                int iTmp = 0;
                int.TryParse(strTmp, out iTmp);
                if (iTmp > 0)
                {
                    this.ID = iTmp;
                }
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                //string Beschreibung = "Exception: " + ex;
                //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Fill()
        {
            string strSQL = "SELECT * FROM ObjPropertyChanges WHERE ID=" + this.ID + ";";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Personal");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                this.ID = iTmp;
                iTmp = 0;
                int.TryParse(dt.Rows[i]["TableId"].ToString(), out iTmp);
                this.TableId = iTmp;

                this.TableName = dt.Rows[i]["TableName"].ToString();
                this.Property = dt.Rows[i]["Property"].ToString();
                this.ValueOld = dt.Rows[i]["ValueOld"].ToString();
                this.ValueNew = dt.Rows[i]["ValueNew"].ToString();
                iTmp = 0;
                int.TryParse(dt.Rows[i]["UserId"].ToString(), out iTmp);
                this.UserId = iTmp;
                this.CreateDate = (DateTime)dt.Rows[i]["CreateDate"];
            }
        }



        /************************************************************************************************************
        *                                   static          
        * **********************************************************************************************************/
        ///<summary>clsArtikelVita / AddImageToArtikel</summary>
        ///<remarks></remarks>
        public static void AddObjPropertyChanges(Globals._GL_USER myGLUser, List<clsObjPropertyChanges> myList)
        {
            string strSql = string.Empty;
            DateTime tmpDateTime = DateTime.Now;
            foreach (clsObjPropertyChanges itm in myList)
            {
                itm.CreateDate = tmpDateTime;
                strSql += itm.AddSql();
            }
            clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "AddObjPropChanges", myGLUser.User_ID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="myArtId"></param>
        /// <returns></returns>
        public static Dictionary<string, clsObjPropertyChanges> GetLastPropertyChangesByArtikel(Globals._GL_USER myGLUser, int myArtId)
        {
            Dictionary<string, clsObjPropertyChanges> tmpReturn = new Dictionary<string, clsObjPropertyChanges>();
            string strSql = "Select * FROM ObjPropertyChanges " +
                                            " WHERE " +
                                                "CreateDate = (SELECT MAX(CreateDate) FROM ObjPropertyChanges WHERE TableName = 'Artikel' and TableId =" + myArtId + ");";
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "ObjectChange", "ObjectChange", myGLUser.User_ID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int iTmp = 0;
                    int.TryParse(row["ID"].ToString(), out iTmp);
                    if (iTmp > 0)
                    {
                        clsObjPropertyChanges ops = new clsObjPropertyChanges();
                        ops.InitCls(myGLUser);
                        ops.ID = iTmp;
                        ops.Fill();
                        if (!tmpReturn.ContainsKey(ops.TableName + "." + ops.Property))
                        {
                            tmpReturn.Add(ops.TableName + "." + ops.Property, ops);
                        }
                    }
                }
            }
            return tmpReturn;
        }

    }
}
