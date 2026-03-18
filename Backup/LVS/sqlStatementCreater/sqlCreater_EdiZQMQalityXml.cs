using LVS.Models;
using System;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_EdiZQMQalityXml
    {
        public EdiZQMQalityXml ediZQMQalityXml { get; set; }
        public sqlCreater_EdiZQMQalityXml()
        {
        }
        public sqlCreater_EdiZQMQalityXml(EdiZQMQalityXml myZqm)
        {
            ediZQMQalityXml = myZqm;
        }

        public string sql_Add()
        {
            string strSql = string.Empty;
            if (ediZQMQalityXml != null)
            {
                strSql = "INSERT INTO EdiZQMQalityXml ([iDocNo], [iDocDate], [Path], [FileName], [WorkspaceId], " +
                                                            "[ArticleId], [IsActive], [iDocXml], [LfsNr] ,[Produktionsnummer], [Description], [WorkspaceXmlRef])" +
                                                            " VALUES " +
                                                            "(" + "" +
                                                                 "'" + ediZQMQalityXml.iDocNo + "'" +
                                                                 ", '" + ediZQMQalityXml.iDocDate + "'" +
                                                                 ", '" + ediZQMQalityXml.Path + "'" +
                                                                 ", '" + ediZQMQalityXml.FileName + "'" +
                                                                 ", " + ediZQMQalityXml.WorkspaceId +
                                                                 ", " + ediZQMQalityXml.ArticleId +
                                                                 ", " + Convert.ToInt32(ediZQMQalityXml.IsActive) +
                                                                 ", '" + ediZQMQalityXml.iDocXml + "'" +
                                                                 ", '" + ediZQMQalityXml.LfsNr + "'" +
                                                                 ", '" + ediZQMQalityXml.Produktionsnummer + "'" +
                                                                  ", '" + ediZQMQalityXml.Description + "'" +
                                                                  ", '" + ediZQMQalityXml.WorkspaceXmlRef + "'" +
                                                             ") ";
                //strSql = strSql + "Select @@IDENTITY as 'ID'; ";
            }
            return strSql;
        }
        /// <summary>
        ///             Update
        /// </summary>
        public string sql_Update()
        {
            string strSql = string.Empty;
            if (ediZQMQalityXml != null)
            {
                strSql = "Update EdiZQMQalityXml SET " +
                                " [iDocNo] = '" + ediZQMQalityXml.iDocNo + "'" +
                                ", [iDocDate] = '" + ediZQMQalityXml.iDocDate + "'" +
                                ", [Path] = '" + ediZQMQalityXml.Path + "'" +
                                ", [FileName] = '" + ediZQMQalityXml.FileName + "'" +
                                ", [WorkspaceId] = " + ediZQMQalityXml.WorkspaceId +
                                ", [ArticleId] = " + ediZQMQalityXml.ArticleId +
                                ", [IsActive] = " + Convert.ToInt32(ediZQMQalityXml.IsActive) +
                                ", [iDocXml] = '" + ediZQMQalityXml.iDocXml + "' " +
                                ", [LfsNr] = '" + ediZQMQalityXml.LfsNr + "'" +
                                ", [Produktionsnummer] = '" + ediZQMQalityXml.Produktionsnummer + "' " +
                                ", [Description] = '" + ediZQMQalityXml.Description + "'" +
                                ", [WorkspaceXmlRef] = '" + ediZQMQalityXml.WorkspaceXmlRef + "'" +

                                " WHERE ID=" + ediZQMQalityXml.Id + " ;";
            }
            return strSql;
        }
        ///// <summary>
        /////             Update
        ///// </summary>
        //public string sql_Update_IsActive()
        //{
        //    string strSql = string.Empty;
        //    if (ediZQMQalityXml != null)
        //    {
        //        strSql = "Update EdiZQMQalityXml SET " +
        //                        "WorkspaceId = " + ediZQMQalityXml.WorkspaceId +
        //                        ", ArticleId = " + ediZQMQalityXml.ArticleId +
        //                        ", IsActive= " + Convert.ToInt32(ediZQMQalityXml.IsActive) + " " +
        //                        ", [LfsNr] = '" + ediZQMQalityXml.LfsNr + "'" +
        //                        ", [Produktionsnummer] = '" + ediZQMQalityXml.Produktionsnummer + "'" +
        //                        ", [Description] = '" + ediZQMQalityXml.Description + "'" +
        //                        ", [WorkspaceXmlRef] = '" + ediZQMQalityXml.WorkspaceXmlRef + "'" +

        //                        " WHERE ID=" + ediZQMQalityXml.Id + " ; ";
        //    }
        //    return strSql;
        //}
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_GetItem()
        {
            string strSql = string.Empty;
            if (ediZQMQalityXml != null)
            {
                strSql = "SELECT * FROM EdiZQMQalityXml WHERE ID=" + ediZQMQalityXml.Id + "; ";
            }
            return strSql;
        }
        /// <summary>
        ///             Exist 
        ///             Check, ob der Artikel sich im SPL befindet
        /// </summary>
        public string sql_Exist()
        {
            string strSql = string.Empty;
            if (ediZQMQalityXml != null)
            {
                strSql = "SELECT Top(1) * FROM EdiZQMQalityXml ";
                strSql += "WHERE ";
                strSql += "IDocNo ='" + ediZQMQalityXml.iDocNo + "' ";
                strSql += "AND LfsNr ='" + ediZQMQalityXml.LfsNr + "' ";
                strSql += "AND Produktionsnummer='" + ediZQMQalityXml.Produktionsnummer + "' ";
            }
            return strSql;
        }
        /// <summary>
        ///             GET List
        /// </summary>
        public string sql_GetList()
        {
            string strSql = string.Empty;
            if (ediZQMQalityXml != null)
            {
                strSql = "SELECT * FROM EdiZQMQalityXml WHERE IsActive=1; ";
            }
            return strSql;
        }
        /// <summary>
        ///             GET List
        /// </summary>
        public string sql_GetListByDate(DateTime mySelectedDate)
        {
            string strSql = string.Empty;
            if (ediZQMQalityXml != null)
            {
                strSql = "SELECT * FROM EdiZQMQalityXml WHERE iDocDate > '" + mySelectedDate.Date.AddDays(-1).ToString() + "';";
            }
            return strSql;
        }
        /// <summary>
        ///             GET_Main
        /// </summary>
        public string sql_Get_Main()
        {
            string strSql = string.Empty;
            if (ediZQMQalityXml != null)
            {
                strSql = "SELECT * FROM EdiZQMQalityXml";
            }
            return strSql;
        }
        /// <summary>
        ///             GET List
        /// </summary>
        public string sql_GetListActivItemsFrom(DateTime myDate)
        {
            string strSql = string.Empty;
            if (ediZQMQalityXml != null)
            {
                strSql = "SELECT * FROM EdiZQMQalityXml WHERE Created >= CAST('" + myDate.AddDays(-1).Date.ToString("dd.MM.yyyy") + "'; ";
            }
            return strSql;
        }
        /// <summary>
        ///             GET List by LfsNr und Produktionsnummer (auch deaktiviert)
        /// </summary>
        public string sql_GetListByLfsNrAndProductionNo(string myLfsNo, string myProductionNo)
        {
            string strSql = string.Empty;
            if (
                    (!myLfsNo.Equals(string.Empty)) ||
                    (!myProductionNo.Equals(string.Empty))
               )
            {
                strSql = "SELECT * FROM EdiZQMQalityXml ";
                strSql += " WHERE ";
                strSql += "LfsNr = '" + myLfsNo + "' ";
                strSql += "OR ";
                strSql += "Produktionsnummer = '" + myProductionNo + "' ";
            }
            return strSql;
        }

    }
}
