using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Sped4.Classes
{
    public class clsSQLcon
    {
        private SqlConnection cSQLCon = new SqlConnection("");

        private string _server;

        private string _database;

        private string _user;

        private string _pw;

        public string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }

        public string Database
        {
            get
            {
                return _database;
            }
            set
            {
                _database = value;
            }
        }

        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        public string Password
        {
            get
            {
                return _pw;
            }
            set
            {
                _pw = value;
            }
        }

        public SqlConnection Connection
        {
            get
            {
                return cSQLCon;
            }
            set
            {
                cSQLCon = value;
            }
        }

        public void Open()
        {
            if (cSQLCon.State == ConnectionState.Closed)
            {
                cSQLCon.Open();
            }
        }

        public void Close()
        {
            cSQLCon.Close();
        }

        public bool init()
        {
            try
            {
                cSQLCon.ConnectionString = ("Data Source = "
                            + (Server + ("; " + ("Initial Catalog = "
                            + (Database + ("; " + ("Persist Security Info = True; " + ("User ID = "
                            + (User + ("; " + ("Password = " + Password)))))))))));
                cSQLCon.Open();
                cSQLCon.Close();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            return true;
        }
    }

}
