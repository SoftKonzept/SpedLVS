using LVS;
using LVS.Helper;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Sped4.Controls.ASNCenter
{
    public partial class ctrVDAClientOutUpdate : UserControl
    {
        public Globals._GL_USER GLUser;
        internal ctrASNMain _ctrAsnMain;
        internal List<string> SqlServerList { get; set; } = new List<string>();
        internal DataTable dtSqlResult { get; set; } = new DataTable();
        public ctrVDAClientOutUpdate(ctrASNMain ctrAsnMain)
        {
            InitializeComponent();
            _ctrAsnMain = ctrAsnMain;
        }

        public void InitCtr()
        {
            //-- get SQL Server list
            SqlServerList = helper_sqlServers.GetListForSqlServers();
            comboSqlServerLists.DataSource = SqlServerList;

            //btnImport.Enabled = false;
        }
        private void InitDgvSqlResult()
        {
            this.dgvSqlResult.DataSource = dtSqlResult;
            this.dgvSqlResult.BestFitColumns();
        }

        private clsSQLconComDiverse GetSqlDiv()
        {
            clsSQLconComDiverse sqlDiv = new clsSQLconComDiverse(string.Empty, string.Empty, string.Empty, string.Empty);
            string strServer = comboSqlServerLists.SelectedItem.ToString();
            string strDB = comboDatabase.SelectedItem.ToString();
            string strUser = comboUser.SelectedItem.ToString();
            string strPass = comboPass.SelectedItem.ToString();

            if (
                    (!strServer.Equals(string.Empty)) &&
                    (!strDB.Equals(string.Empty)) &&
                    (!strUser.Equals(string.Empty)) &&
                    (!strPass.Equals(string.Empty))
                )
            {
                sqlDiv = new clsSQLconComDiverse(strServer, strDB, strUser, strPass);
            }
            else
            {
                sqlDiv.ConnectionOK = false;
            }
            return sqlDiv;
        }

        private void btnSqlExecute_Click(object sender, EventArgs e)
        {
            string strSql = string.Empty;
            if (tbSqlStatement.Text.Trim().Length > 0)
            {
                clsSQLconComDiverse sqlDiv = GetSqlDiv();
                strSql = tbSqlStatement.Text.Replace("\n", " ").Trim();

                dtSqlResult = clsSQLconComDiverse.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "SQL", "SQLResult", 1, sqlDiv);
                InitDgvSqlResult();
            }
            else
            {
                string mes = "Die SQL - Abfrage ist leer!";
                clsMessages.Allgemein_InfoTextShow(mes);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (dtSqlResult.Rows.Count > 0)
            {
                clsSQLconComDiverse sqlCon = GetSqlDiv();
                List<string> sqlList = new List<string>();

                foreach (DataRow dr in dtSqlResult.Rows)
                {
                    int iVDAClientOutId = 0;
                    int.TryParse(dr["ID"].ToString(), out iVDAClientOutId);
                    if (iVDAClientOutId > 0)
                    {
                        try
                        {
                            //-- class with value to Update
                            VDAClientValues valueUp = VDAClientValueViewData.GetVdaClientValueToImport(sqlCon, iVDAClientOutId);
                            if (valueUp.Id > 0)
                            {
                                valueUp.Description = "Up " + DateTime.Now.ToString();
                                //--- value to Update in prod. db
                                VDAClientValueViewData vd = new VDAClientValueViewData(valueUp);
                                //vd.Update();
                                string s = vd.sql_Update;
                                sqlList.Add(s);
                            }
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                        }
                    }
                }

                if (dtSqlResult.Rows.Count == sqlList.Count)
                {
                    string strSql = string.Empty;
                    foreach (var s in sqlList)
                    {
                        strSql += s.ToString() + " ";
                    }
                    try
                    {
                        if (strSql.Length > 0)
                        {
                            bool bOK = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "Update", 1);
                            if (bOK)
                            {
                                string mes = "Das SQL - Update wurde erfolgreich durchgeführt!";
                                clsMessages.Allgemein_InfoTextShow(mes);
                            }
                            else
                            {
                                string mes = "Fehler! - Update nicht durchgeführt!";
                                clsMessages.Allgemein_InfoTextShow(mes);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                    }
                }
                else
                {
                    string mes = "Fehler! - Datensätze Tabelle und Sql-Statement ist unterschiedlich. Update nicht durchgeführt!";
                    clsMessages.Allgemein_InfoTextShow(mes);
                }
            }
            else
            {
                string mes = "Eine Datensätze vorhanden!";
                clsMessages.Allgemein_InfoTextShow(mes);
            }
        }
    }
}
