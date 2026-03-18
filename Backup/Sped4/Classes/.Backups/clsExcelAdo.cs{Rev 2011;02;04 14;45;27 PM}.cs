using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections;

namespace Sped4.Classes
{
  class clsExcelAdo
  {
    private string m_strClientId;
    private bool m_bGlobal;


    public clsExcelAdo(string strClientId)
    {
      this.m_bGlobal = false;
      this.m_strClientId = strClientId;
    }

    public clsExcelAdo(string strClientId, bool bGlobal)
    {
      this.m_bGlobal = bGlobal;
      this.m_strClientId = strClientId;
    }

    public void SetGlobal( bool bGlobal )
    {
      this.m_bGlobal = bGlobal;
    }

    public string GetSheetPath()
    {
      string strDir = "";
      if( this.m_bGlobal )
        strDir = System.Web.HttpContext.Current.Server.MapPath("~/superuser/files/xls");
      else
        strDir = System.Web.HttpContext.Current.Server.MapPath("~/clientdata/" +
          m_strClientId + "/xls");
//        strDir = System.Web.HttpContext.Current.Server.MapPath("/eventline/clientdata/" + 
          
      if( !Directory.Exists( strDir ) )
        strDir = "";
      else
        strDir += "\\";
      return(strDir);
    }

    public bool DeleteFile( string strFileName )
    {
      string strFileAbs = GetSheetPath();
      try
      {
        if( strFileAbs != "" )
        {
          strFileAbs += strFileName;
          if( File.Exists(strFileAbs) )
            File.Delete(strFileAbs);
        }
      }
      catch
      {return(false);}
      return(true);
    }


    private string GetExcelCon(string strSheet)
    {
      string strSheetPath = GetSheetPath();
      string strConn = "";
      if( strSheetPath != "" )
      {
        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +
          "Data Source=" + strSheetPath + strSheet + ";" +
          "Extended Properties=\"Excel 8.0;\"";  
      }
      return(strConn);
    }

    private string CreateWorkbook(ArrayList alFields, ArrayList alTypes, string strWorkBookName, string strFileName)
    {
      string strRes = "";
      string strSql = "";
      OleDbConnection con = new OleDbConnection();
      OleDbCommand cmd = new OleDbCommand();
      try
      {
        if( alFields.Count > 0 )
        {
          strSql = "CREATE TABLE " + strWorkBookName + "(";
          for( int iFieldCount=0; iFieldCount<alFields.Count; iFieldCount++)
          {
            strSql += alFields[iFieldCount] + " " +
              alTypes[iFieldCount];
            if( iFieldCount+1 < alFields.Count )
              strSql += ", ";
          }
          strSql += ")";
          con.ConnectionString = GetExcelCon(strFileName);
          cmd.Connection = con;
          cmd.CommandText = strSql;
          con.Open();
          cmd.ExecuteNonQuery();
        }
      }
      catch( Exception ex )
      {
        strRes = ex.Message.ToString();
      }
      if( cmd.Connection.State != ConnectionState.Closed )
        cmd.Connection.Close();
      if( con.State != ConnectionState.Closed )
        con.Close();
      return(strRes);
    }
/***
    private string InsertData( SqlDataReader rd, ArrayList alFields, ArrayList alTypes, string strWorkBookName, string strFileName )
    {
      string strRes = "";
      string strSql = "";
      OleDbConnection con = new OleDbConnection();
      OleDbCommand cmd = new OleDbCommand();
      ArrayList alOut = new ArrayList();

      try
      {
        while( rd.Read() )
        {
          if( alFields.Count > 0 )
          {
            strSql = "INSERT INTO [" + strWorkBookName.Trim() + "$] (";
            for( int iFieldCount=0; iFieldCount<alFields.Count; iFieldCount++)
            {
              strSql += alFields[iFieldCount] + " ";
              if( iFieldCount+1 < alFields.Count )
                strSql += ", ";
            }
            strSql += ") VALUES (";
            for( int iFieldCount=0; iFieldCount<alFields.Count; iFieldCount++)
            {
              if( alTypes[iFieldCount].ToString() != SqlDbType.Int.ToString() )
                strSql += "'" + rd.GetValue(rd.GetOrdinal(alFields[iFieldCount].ToString()))  + "'";
              else
                strSql += rd.GetValue(rd.GetOrdinal(alFields[iFieldCount].ToString()))  + " ";
              if( iFieldCount+1 < alFields.Count )
                strSql += ", ";
            }
            strSql += ")";
            alOut.Add(strSql);
          }
        }

        con.ConnectionString = GetExcelCon(strFileName);
        cmd.Connection = con;
        con.Open();
        for( int iRecords=0;iRecords<alOut.Count;iRecords++)
        {
          cmd.CommandText = alOut[iRecords].ToString();
          cmd.ExecuteNonQuery();
        }
      }
      catch( Exception ex )
      {
        strRes = ex.Message.ToString();
      }
      if( cmd.Connection.State != ConnectionState.Closed )
        cmd.Connection.Close();
      if( con.State != ConnectionState.Closed )
        con.Close();
      return(strRes);
    }
****/
    private string InsertData(DataTable dt, ArrayList alFields, ArrayList alTypes, 
      string strWorkBookName, string strFileName)
    {
      string strRes = "";
      string strSql = "";
      OleDbConnection con = new OleDbConnection();
      OleDbCommand cmd = new OleDbCommand();
      ArrayList alOut = new ArrayList();
      try
      {
        for( Int32 iRow=1; iRow<dt.Rows.Count; iRow ++)
        {
          if (alFields.Count > 0)
          {
            strSql = "INSERT INTO [" + strWorkBookName.Trim() + "$] (";
            for (int iFieldCount = 0; iFieldCount < alFields.Count; iFieldCount++)
            {
              strSql += alFields[iFieldCount] + " ";
              if (iFieldCount + 1 < alFields.Count)
                strSql += ", ";
            }
            strSql += ") VALUES (";
            for (int iFieldCount = 0; iFieldCount < alFields.Count; iFieldCount++)
            {
//              if (alTypes[iFieldCount].ToString() != SqlDbType.Int.ToString())
                strSql += "'" + dt.Rows[iRow].ItemArray[iFieldCount].ToString() + "'";
/*
              else
                strSql += rd.GetValue(rd.GetOrdinal(alFields[iFieldCount].ToString())) + " ";
*/ 
              if (iFieldCount + 1 < alFields.Count)
                strSql += ", ";
            }
            strSql += ")";
            alOut.Add(strSql);
          }
        }

        con.ConnectionString = GetExcelCon(strFileName);
        cmd.Connection = con;
        con.Open();
        for (int iRecords = 0; iRecords < alOut.Count; iRecords++)
        {
          cmd.CommandText = alOut[iRecords].ToString();
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        strRes = ex.Message.ToString();
      }
      if (cmd.Connection.State != ConnectionState.Closed)
        cmd.Connection.Close();
      if (con.State != ConnectionState.Closed)
        con.Close();
      return (strRes);
    }

    private string GetFilename( string strName )
    {
      string strRes = strName + "_" +
        DateTime.Now.ToShortDateString() + "_" + 
        DateTime.Now.ToShortTimeString();
      strRes = strRes.Replace(":", "-").Replace("/", "-").Replace("\\","-").Replace("?","-");
      strRes = strRes.Replace("*","-").Replace("\"","-").Replace("|","-").Replace(".","_");
      return(strRes);
    }

/*****

    public string ExportToExcel( string strSql, string strName )
    {
      string strRes = "";
      DbAcc dac = new DbAcc(strSql);
      SqlDataReader rd = null;
      try
      {
        dac.RunReader(ref rd);
        if( rd.HasRows )
        {
          ArrayList alFields = new ArrayList();
          ArrayList alTypes = new ArrayList(); 
          for( int iFieldCount=0;iFieldCount<rd.FieldCount;iFieldCount++)
          {
            alFields.Add(rd.GetName(iFieldCount));
            alTypes.Add(rd.GetDataTypeName(iFieldCount));
          }
          string strFileName = GetFilename(strName);
          while( strRes == "" )
          {
            strRes = CreateWorkbook( alFields, alTypes, strName, strFileName );
            strRes = InsertData( rd, alFields, alTypes, strName, strFileName );
            if( !this.m_bGlobal )
            {
              strRes = "Sie können Die Datei unter 'Meine Dateien' <br>" +
                strFileName + " abrufen oder direkt " +
                "<A href=\"../clientdata/" + m_strClientId + "/xls/" + strFileName + 
                ".xls\" target=\"_new\"><b>HIER</b></a> &ouml;ffnen.";
            }
            else
            {
              util ut = new util();
              strRes = "<b>Eine Email mit der Datei wird an " + 
                ut.GetSString("EMAIL") 
                + " gesendet!</b>";              
            }
          }
        }
      }
      catch( Exception ex )
      {
        strRes = ex.Message.ToString();
      }
      dac.CloseUp(ref rd);
      return(strRes);
    }
******/
    public string ExportToExcel(DataTable dt, string strFileName)
    {
      string strRes = "";
      try
      {
        if (dt.Rows.Count>0)
        {
          ArrayList alFields = new ArrayList();
          ArrayList alTypes = new ArrayList();
          for (int iFieldCount = 0; iFieldCount < dt.Columns.Count; iFieldCount++)
          {
            alFields.Add(dt.Columns[iFieldCount].ColumnName.ToString());
            alTypes.Add(dt.Columns[iFieldCount].DataType);
          }
//          string strFileName = GetFilename(strName);
          string strName = Path.GetFileNameWithoutExtension(strFileName);
          while (strRes == "")
          {
            strRes = CreateWorkbook(alFields, alTypes, strName, strFileName);
            strRes = InsertData(dt, alFields, alTypes, strName, strFileName);
          }

        }
      }
      catch (Exception ex)
      {
        strRes = ex.Message.ToString();
      }
      return (strRes);
    }
	}

/*
CExcelAdo exa = new CExcelAdo(strClientId);
exa.ExportToExcel( BuildSql(true), "Adressen" );
*/ 

}