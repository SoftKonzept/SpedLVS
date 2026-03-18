using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LVS
{
    public class clsASNArtFieldAssignment
    {
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
        public decimal Receiver { get; set; }
        public int AbBereichID { get; set; }
        public string ASNField { get; set; }
        public string ArtField { get; set; }
        public bool IsDefValue { get; set; }
        public string DefValue { get; set; }
        public string CopyToField { get; set; }
        public string FormatFunction { get; set; }

        public clsADR AdrSender
        {
            get
            {
                clsADR tmpADR = new clsADR();
                if (this.Sender > 0)
                {
                    tmpADR.InitClass(this._GL_User, this._GL_System, this.Sender, true);
                }
                return tmpADR;
            }
        }
        public clsADR AdrReceiver
        {
            get
            {
                clsADR tmpADR = new clsADR();
                if (this.Receiver > 0)
                {
                    tmpADR.InitClass(this._GL_User, this._GL_System, this.Receiver, true);
                }
                return tmpADR;
            }
        }

        public bool IsGlobalFieldVar { get; set; }
        public string GlobalFieldVar { get; set; }
        public string SubASNField { get; set; }

        /******************************************************************************
         *                          Methoden / Procedure
         * ***************************************************************************/
        ///<summary>clsASNArtFieldAssignment / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSystem;
        }
        /// <summary>
        ///             clsASNArtFieldAssignment / InitClass
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="myGLSystem"></param>
        /// <param name="mySenderId"></param>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, decimal mySenderId)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSystem;
            this.Sender = mySenderId;
        }
        /// <summary>
        ///             clsASNArtFieldAssignment / InitClass
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="myGLSystem"></param>
        /// <param name="mySenderId"></param>
        public void InitClassByID(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, decimal myId)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSystem;
            if (myId > 0)
            {
                this.ID = myId;
                this.Fill();
            }
        }
        ///<summary>clsASNArtFieldAssignment / Copy</summary>
        ///<remarks></remarks>
        public clsASNArtFieldAssignment Copy()
        {
            return (clsASNArtFieldAssignment)this.MemberwiseClone();
        }
        ///<summary>clsASNArtFieldAssignment / Fill</summary>
        ///<remarks></remarks>>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ASNArtFieldAssignment WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ASNArtFieldAss");
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                this.ID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Sender"].ToString(), out decTmp);
                this.Sender = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Receiver"].ToString(), out decTmp);
                this.Receiver = decTmp;
                this.ASNField = dt.Rows[i]["ASNField"].ToString();
                this.ArtField = dt.Rows[i]["ArtField"].ToString();
                this.IsDefValue = (bool)dt.Rows[i]["IsDefValue"];
                this.DefValue = dt.Rows[i]["DefValue"].ToString();
                this.CopyToField = dt.Rows[i]["CopyToField"].ToString();
                this.FormatFunction = dt.Rows[i]["FormatFunction"].ToString();
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["AbBereichID"].ToString(), out iTmp);
                this.AbBereichID = iTmp;

                this.IsGlobalFieldVar = (bool)dt.Rows[i]["IsGlobalFieldVar"];
                this.GlobalFieldVar = dt.Rows[i]["GlobalFieldVar"].ToString();
                this.SubASNField = dt.Rows[i]["SubASNField"].ToString();
            }
        }
        /// <summary>
        ///             Save Value
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            string strSql = string.Empty;
            strSql = "Update ASNArtFieldAssignment SET " +
                                "Sender = " + (int)this.Sender +
                                ", Receiver = " + (int)this.Receiver +
                                ", ASNField = '" + this.ASNField + "'" +
                                ", ArtField = '" + this.ArtField + "'" +
                                ", IsDefValue = " + Convert.ToInt32(this.IsDefValue) +
                                ", DefValue = '" + this.DefValue + "'" +
                                ", CopyToField = '" + this.CopyToField + "'" +
                                ", FormatFunction = '" + this.FormatFunction + "'" +
                                ", AbBereichID = " + Convert.ToInt32(this.AbBereichID) +
                                ", IsGlobalFieldVar = " + Convert.ToInt32(this.IsGlobalFieldVar) +
                                ", GlobalFieldVar = '" + this.GlobalFieldVar + "'" +
                                ", SubASNField = '" + this.SubASNField + "'" +

                                " WHERE ID = " + ID + "; ";
            bool bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "Save", this.BenutzerID);
            return bReturn;
        }
        ///<summary>clsASNArtFieldAssignment / GetArtikelFieldAssignment</summary>
        ///<remarks></remarks>>
        public Dictionary<string, clsASNArtFieldAssignment> GetArtikelFieldAssignment(decimal mySender, decimal myReceiver, int myAbBereichID)
        {
            Dictionary<string, clsASNArtFieldAssignment> retDict = new Dictionary<string, clsASNArtFieldAssignment>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ASNArtFieldAssignment " +
                                    "WHERE " +
                                        "Sender=" + mySender +
                                        " AND Receiver=" + myReceiver +
                                        " AND AbBereichID=" + myAbBereichID +
                                        ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ASNArtFieldAssignment");
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsASNArtFieldAssignment tmp = new clsASNArtFieldAssignment();
                    tmp._GL_User = this._GL_User;
                    tmp.ID = decTmp;
                    tmp.Fill();
                    if (!retDict.ContainsKey(tmp.ASNField))
                    {
                        retDict.Add(tmp.ASNField, tmp);
                    }
                }
            }
            return retDict;
        }
        ///<summary>clsASNArtFieldAssignment / GetArtikelFieldAssignment</summary>
        ///<remarks></remarks>>
        public Dictionary<string, clsASNArtFieldAssignment> GetArtikelFieldAssignmentCopyFields(decimal mySender, decimal myReceiver, int myAbBereichID)
        {
            Dictionary<string, clsASNArtFieldAssignment> retDict = new Dictionary<string, clsASNArtFieldAssignment>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ASNArtFieldAssignment " +
                                    "WHERE " +
                                        "Sender=" + mySender +
                                        " AND Receiver=" + myReceiver +
                                        " AND AbBereichID=" + myAbBereichID +
                                        " AND (CopyToField<>'' OR CopyToField IS NOT NULL);";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ASNArtFieldAssignment");
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsASNArtFieldAssignment tmp = new clsASNArtFieldAssignment();
                    tmp._GL_User = this._GL_User;
                    tmp.ID = decTmp;
                    tmp.Fill();
                    if (!retDict.Keys.Contains(tmp.ASNField.ToString()))
                    {
                        retDict.Add(tmp.ASNField, tmp);
                    }
                }
            }
            return retDict;
        }
        /// <summary>
        ///             Delete Item
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            string strSql = string.Empty;
            strSql = "DELETE ASNArtFieldAssignment WHERE ID=" + (int)this.ID + ";";
            bool bReturn = clsSQLcon.ExecuteSQL_GetValueBool(strSql, this.BenutzerID);
            return bReturn;
        }
        /// <summary>
        ///         
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="mySenderAdrID"></param>
        /// <returns></returns>
        public static DataTable GetASNArtFieldAssignmentsBySender(Globals._GL_USER myGLUser, decimal mySenderAdrID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ASNArtFieldAssignment WHERE Sender=" + (int)mySenderAdrID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "ASNArtFieldAss");
            return dt;
        }
        /// <summary>
        ///         
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="mySenderAdrID"></param>
        /// <returns></returns>
        public static bool ExistbySender(Globals._GL_USER myGLUser, decimal mySenderAdrID)
        {
            string strSql = string.Empty;
            strSql = "SELECT ID FROM ASNArtFieldAssignment WHERE Sender=" + (int)mySenderAdrID + ";";
            bool bReturn = clsSQLcon.ExecuteSQL_GetValueBool(strSql, myGLUser.User_ID);
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySourceAdrId"></param>
        /// <param name="myDestAdrId"></param>
        /// <param name="myUserId"></param>
        /// <returns></returns>
        public static bool InsertShapeByRefAdr(List<int> myListID, int myDestAdrId, decimal myUserId, decimal myAbBereichID)
        {
            bool bReturn = false;
            string strSql = string.Empty;
            strSql = " INSERT INTO ASNArtFieldAssignment ([Sender] ,[Receiver],[ASNField],[ArtField] ,[IsDefValue] " +
                                                          " ,[DefValue],[CopyToField] ,[FormatFunction] ,[AbBereichID]" +
                                                          " ,[IsGlobalFieldVar] ,[GlobalFieldVar], [SubASNField]) " +
                    "SELECT " +
                            myDestAdrId +
                            ", a.Receiver " +
                            ", a.ASNField " +
                            ", a.ArtField" +
                            ", a.IsDefValue " +
                            ", a.DefValue " +
                            ", a.CopyToField " +
                            ", a.FormatFunction " +
                            // ", a.AbBereichID " +
                            ", " + (int)myAbBereichID +
                            ", a.IsGlobalFieldVar " +
                            ", a.GlobalFieldVar " +
                            ", a.SubASNField " +

                                " FROM ASNArtFieldAssignment a " +
                                    "WHERE " +
                                    " a.ID in (" + string.Join(",", myListID.ToArray()) + ");";
            //"a.Sender =" + mySourceAdrId + "; ";
            bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "InsertASNArtFieldAssignment", myUserId);
            return bReturn;
        }

    }
}
