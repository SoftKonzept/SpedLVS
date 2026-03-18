using System;
using System.Collections.Generic;
using Common.Models;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Article
    {

        public sqlCreater_Article()
        { 
        
        }
 

        public string sqlString_Article_UpdateIdentifiedByScan(List<Common.Models.Articles> myArticleList)
        {
            List<int> myIdList = new List<int>();
            foreach(var i in myArticleList) 
            {
                if (!myIdList.Contains(i.Id))
                { 
                    myIdList.Add(i.Id);
                }
            }

            string strSql = string.Empty;
            if (myIdList.Count > 0) 
            {
                strSql = "Update Artikel SET ";
                strSql += " IdentifiedByScan='" + DateTime.Now.ToString() + "' ";
                strSql += " WHERE ID IN (" + string.Join(", ", myIdList.ToArray()) + "); ";
            }
            return strSql;
        }
    }
}
