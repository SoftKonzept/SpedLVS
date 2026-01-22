using Common.Enumerations;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.SqlStatementCreater
{
    public class sqlCreater_InventoryArticles
    {
        public string Sql_Upate { get; set; }
        public sqlCreater_InventoryArticles(DatabaseManipulations databaseManuipulation)
        {
            switch (databaseManuipulation.Action)
            {
                case enumDatabaseAction.Add:
                    break;
                case enumDatabaseAction.Update:
                    sqlCreate_Update(databaseManuipulation);
                    break;
                case enumDatabaseAction.Delete:
                    break;
            }
        }

        private List<string> sqlList = new List<string>();

        /// <summary>
        ///             create sql update statement
        /// </summary>
        /// <param name="databaseManuipulation"></param>
        private void sqlCreate_Update(DatabaseManipulations databaseManuipulation)
        {
            string strReturn = string.Empty;
            if (databaseManuipulation.ActionList.Count > 0)
            {
                List<string> sqlList = new List<string>();
                PropertyInfo[] propInfos = GetPropertyInf();
                foreach (var info in propInfos)
                {
                    DatabaseTableProperties dbp = databaseManuipulation.ActionList.Where(x => x.FieldName == info.Name).FirstOrDefault();
                    if (dbp != null)
                    {
                        string s = String.Empty;
                        switch (info.Name)
                        {
                            case "Description":
                                s = info.Name + " = " + dbp.Value.ToString();
                                break;
                            case "Status":
                                s = "[" + info.Name + "] = " + Enum.Parse(typeof(enumInventoryArticleStatus), dbp.Value);
                                break;
                            case "Text":
                                s = info.Name + "  = " + Enum.Parse(typeof(enumInventoryArticleStatus), dbp.Value);
                                break;
                            case "Scanned":
                                s = info.Name + "  = '" + Convert.ToDateTime(dbp.Value) + "' ";
                                break;
                            case "ScannedUserId":
                                s = info.Name + "  = " + dbp.Value.ToString();
                                break;
                        }
                        sqlList.Add(s);
                    }
                }
                string strHead = "Update InventoryArticle SET ";
                string strProp = string.Empty;
                string strWhere = " where Id=" + databaseManuipulation.TableId.ToString();
                int iIndex = 0;
                foreach (var s in sqlList)
                {
                    if (iIndex < sqlList.Count - 1)
                    {
                        strProp += s + ", ";
                    }
                    else
                    {
                        strProp += s;
                    }
                    iIndex++;
                }
                strReturn = strHead + strProp + strWhere;
            }

            Sql_Upate = strReturn;
        }

        private PropertyInfo[] GetPropertyInf()
        {
            Type t = typeof(InventoryArticles);
            // Get the public properties.
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return propInfos;
        }
    }
}
