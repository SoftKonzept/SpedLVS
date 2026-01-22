using LVS.Constants;
using System;
using System.Collections.Generic;

namespace LVS.Print
{
    public class ErrorAnalyse
    {
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Log { get; set; } = new List<string>();


        public string FileNameError
        {
            get
            {
                return DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_Error.txt";
            }
        }
        public string FileNameLog
        {
            get
            {
                return DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_Log.txt";
            }
        }
        public ErrorAnalyse()
        {
        }
        public void AddLog(string myLog)
        {
            Log.Add(myLog);
        }
        public void AddErrors(string myErrors)
        {
            Errors.Add(myErrors);
        }
        public void WriteToHdd_Log()
        {

            //string FilePathToWrite = clsReportDocSetting.const_localTempReportPath + this.FileNameLog;
            string FilePathToWrite = constValue_Report.const_localTempReportPath + this.FileNameLog;
            helper_IOFile.WriteFileInLine(FilePathToWrite, Log);
        }

        public void AddListToLog(List<string> myList)
        {
            foreach (string myLog in myList)
            {
                Log.Add(myLog);
            }
        }
        public void WriteToHdd_Error()
        {
            //string FilePathToWrite = clsReportDocSetting.const_localTempReportPath + this.FileNameError;
            string FilePathToWrite = constValue_Report.const_localTempReportPath + this.FileNameError;
            helper_IOFile.WriteFileInLine(FilePathToWrite, Errors);
        }



    }
}
