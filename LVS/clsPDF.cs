using AxAcroPDFLib;
using System;
using System.Diagnostics;
using System.IO;

namespace LVS
{
    public class clsPDF
    {

        public static bool printPDF(string myPath)
        {
            //AcroPDFClass pdf = new AcroPDFClass();
            //AcroPDFLib.AcroPDFClass pdf = new AcroPDFLib.AcroPDFClass();
            AxAcroPDF pdf = new AxAcroPDF();
            bool bExists = pdf.LoadFile(myPath);
            //if (bExists) 
            //{
            pdf.Print();

            //}


            return bExists;

        }

        public static bool printExistingPDF(string myPath)
        {
            bool breturn = false;
            myPath = myPath.Replace("/", "_");
            //string path = "\\\\SRV2012R2\\Daten\\Test\\";
            string endung = ".pdf";
            string path = "\\\\Sqlserver2014\\lvs\\SalzgitterTransportScheine\\";
            string file = path + myPath + endung; // wert in ini übernehmen
            if (File.Exists(file))
            {
                try
                {
                    Process proc = new Process();
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.Verb = "print";
                    proc.StartInfo.FileName = file;
                    proc.Start();
                    proc.WaitForExit(2000);
                    proc.CloseMainWindow();
                    //proc.Close();
                    if (!proc.HasExited)
                        proc.Kill();
                    string datetime = DateTime.Now.ToString().Replace(":", "_").Replace(".", "_");
                    string filepathMove = path + "printed\\" + myPath + "_" + datetime + endung;
                    File.Move(file, filepathMove);
                    breturn = true;
                }
                catch (Exception ex)
                {

                }
            }
            return breturn;

        }

    }
}
