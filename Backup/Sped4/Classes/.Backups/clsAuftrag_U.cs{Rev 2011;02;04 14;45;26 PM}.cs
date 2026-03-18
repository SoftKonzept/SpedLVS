using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;
using Sped4;


namespace Sped4.Classes
{
    class clsAuftrag_U
    {
        private int _ID;
        private string _ViewID;
        private int _UA_ID;
        private int _A_ID;
        private int _KD_ID;
        private int _Gut_ID;
        private DateTime _T_Date;
        private DateTime _T_KW;
        private string _Status;
        private int _B_ID;
        private int _E_ID;
        private int _SU_ID;
        private int _RG_ID;




        private decimal _Gewicht;
        private string _Notiz;


        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string ViewID
        {
            get { return _ViewID; }
            set { _ViewID = value; }
        }
        public int KD_ID
        {
            get { return _KD_ID; }
            set { _KD_ID = value; }
        }
        public DateTime T_Date
        {
            get { return _T_Date; }
            set { _T_Date = value; }
        }
        public DateTime T_KW
        {
            get { return _T_KW; }
            set { _T_KW = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public decimal Gewicht
        {
            get { return _Gewicht; }
            set { _Gewicht = value; }
        }
        public string Notiz
        {
            get { return _Notiz; }
            set { _Notiz = value; }
        }

        //**********************************************************************************
        //--------------              Methoden
        //**********************************************************************************


    }
}
