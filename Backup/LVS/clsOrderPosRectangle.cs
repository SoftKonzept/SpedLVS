using System;
using System.Data;
using System.Data.SqlClient;
//using System.Windows.Forms;


namespace LVS
{
    public class clsOrderPosRectangle
    {
        private decimal _ID;
        private decimal _AuftragID;
        private decimal _AuftragPos;
        private Int32 _x_Pos;
        private Int32 _y_Pos;
        private string _ImageArt;
        private Int32 _PicNum;
        private decimal _AuftragScanID;
        private decimal _OrderPosRecID;
        private DateTime _Date_Add;




        public decimal ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public decimal AuftragID
        {
            get { return _AuftragID; }
            set { _AuftragID = value; }
        }
        public decimal AuftragPos
        {
            get { return _AuftragPos; }
            set { _AuftragPos = value; }
        }
        public Int32 x_Pos
        {
            get { return _x_Pos; }
            set { _x_Pos = value; }
        }
        public Int32 y_Pos
        {
            get { return _y_Pos; }
            set { _y_Pos = value; }
        }
        public string ImageArt
        {
            get { return _ImageArt; }
            set { _ImageArt = value; }
        }
        public Int32 PicNum
        {
            get { return _PicNum; }
            set { _PicNum = value; }
        }
        public decimal AuftragScanID
        {
            get { return _AuftragScanID; }
            set { _AuftragScanID = value; }
        }
        public decimal OrderPosRecID
        {
            get { return _OrderPosRecID; }
            set { _OrderPosRecID = value; }
        }
        public DateTime Date_Add
        {
            get
            {
                _Date_Add = DateTime.Now;
                return _Date_Add;
            }
            set { _Date_Add = value; }
        }

        //
        //
        //
        public void InsertRectanglePosition()
        {
            Date_Add = DateTime.Now;
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = ("INSERT INTO OrderPosRectangle(Auftrag, AuftragPos, x_Pos, y_Pos, PicNum, ImageArt, Date_Add ) " +
                                                "VALUES ('" + AuftragID + "','"
                                                            + AuftragPos + "','"
                                                            + x_Pos + "','"
                                                            + y_Pos + "','"
                                                            + PicNum + "','"
                                                            + ImageArt + "','"
                                                            + Date_Add + "')");

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

            }
            finally
            {
                //MessageBox.Show("Der Adressdatensatz wurde erfolgreich in die Datenbank geschrieben!");

            }
        }
        //
        //
        //
        public DataTable GetRectanglePos()
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                            "ID, " +
                                            "Auftrag, " +
                                            "AuftragPos, " +
                                            "x_Pos, " +
                                            "y_Pos, " +
                                            "PicNum, " +
                                            "ImageArt " +
                                                  "FROM OrderPosRectangle WHERE Auftrag='" + AuftragID + "' AND PicNum='" + PicNum + "'";

            ada.Fill(dataTable);
            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();
            return dataTable;
        }
        //
        //
        //
        //
        //------------------------------ Update opr ---------------------------------
        //
        public void UpdateOPR()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand UpCommand = new SqlCommand();
                UpCommand.Connection = Globals.SQLcon.Connection;

                UpCommand.CommandText = "Update OrderPosRectangle SET " +
                                                                  "x_Pos='" + x_Pos + "', " +
                                                                  "y_Pos='" + y_Pos + "', " +
                                                                  "Date_Add='" + Date_Add + "' " +
                                                                               "WHERE ID='" + OrderPosRecID + "'";

                Globals.SQLcon.Open();
                UpCommand.ExecuteNonQuery();
                UpCommand.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

            }
            finally
            {
                // MessageBox.Show("Update OK!");

            }
        }
        //
        //------- löschen -----------
        //
        public void DeleteRectanglePos()
        {
            if (OrderPosRecID > 0)
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = "DELETE FROM OrderPosRectangle WHERE ID='" + OrderPosRecID + "'";
                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
        }
        //
        //------- löschen nach Auftrag AuftragPOs -----------
        //
        public static void DeleteRectanglePosByAuftragAuftragPos(decimal Auftrag, decimal AuftragPos)
        {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            Command.CommandText = "DELETE FROM OrderPosRectangle WHERE Auftrag='" + Auftrag + "' AND AuftragPos='" + AuftragPos + "'";
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //
        //----------- Check Verwendung ------------------- 
        //
        public static bool IsAuftragAuftragPosIn(decimal Auftrag, decimal AuftragPos)
        {
            bool IsIn = true;
            try
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT ID FROM OrderPosRectangle WHERE Auftrag='" + Auftrag + "' AND AuftragPos='" + AuftragPos + "'";
                Globals.SQLcon.Open();
                if (Command.ExecuteScalar() == null)
                {
                    IsIn = false;
                }
                else
                {
                    IsIn = true;
                }
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            return IsIn;
        }

    }
}
