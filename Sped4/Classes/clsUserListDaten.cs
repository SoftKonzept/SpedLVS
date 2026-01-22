using LVS;
using System;
using System.Data;

namespace Sped4.Classes
{
    class clsUserListDaten
    {
        public Globals._GL_USER _GL_User;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }
        //************************************

        public DataTable dtUserListDaten = new DataTable();

        private decimal _ID;
        private decimal _UserListID;
        private string _Table;
        private string _Column;
        private string _ColViewName;
        private Type _Type;
        private string _Sort;



        public decimal ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public decimal UserListID
        {
            get { return _UserListID; }
            set
            {
                _UserListID = value;
                GetUserListDatenByUserListID();
            }
        }
        public string Table
        {
            get { return _Table; }
            set { _Table = value; }
        }
        public string Column
        {
            get { return _Column; }
            set { _Column = value; }
        }
        public string ColViewName
        {
            get { return _ColViewName; }
            set { _ColViewName = value; }
        }
        public Type Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public string Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }

        /******************************************************************************
         *                          Methoden
         * ****************************************************************************/
        ///<summary>clsUserListDaten / GetUserListAvailable</summary>
        ///<remarks>Ermittelt alle freiggegbenen und vom USer selbst angelegten Listen.</remarks>
        private void GetUserListDatenByUserListID()
        {
            string strSQL = string.Empty;
            strSQL = "Select " +
                                "b.ID" +
                                ", b.UserListID" +
                                ", b.[Table]" +
                                ", b.[Column]" +
                                ", b.ColViewName" +
                                ", b.Type" +
                                ", b.Sort" +

                                " FROM UserList a " +
                                "INNER JOIN UserListDaten b ON b.UserListID=a.ID " +
                                "WHERE " +
                                    "b.UserListID=" + UserListID + "; ";
            dtUserListDaten = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "UserListDaten");
        }


    }
}
