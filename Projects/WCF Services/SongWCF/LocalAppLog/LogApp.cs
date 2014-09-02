using System;
using System.IO;
using System.Text;
using System.Data;
namespace LocalAppLog
{
    public class LogApp
    {
        /// <summary>
        /// Simple Local Log Ojbect (write into a log file)
        /// </summary>
        /// <param name="TheLogMsg">Log Message</param>
        /// <remarks>
        /// To improve this log, we can store the data into database and setup the log level for different instances (Dev mode or Product mode)
        /// </remarks>
        public string Addlog(string TheLogMsg)
        {
            string ReturnMessage = "";
            string TString01 = "";
            string TString02 = "";
            StringBuilder TempString = new StringBuilder();
            //TempString = New StringBuilder("");  //Same as New StringBuilder
            string path = "C:\\Housekeeping.log";
            //string path = "Housekeeping.log";
            string LogMsgString = null;
            try
            {
                TempString.Append(DateTime.Now + " ||");
                TempString.Append(TheLogMsg);
                LogMsgString = TempString.ToString();
                TString02 = "";


                if (File.Exists(path) == false)
                {
                    // Create a file to write to.
                    StreamWriter sw = File.CreateText(path);
                    TString01 = DateTime.Now + " || Log created.";
                    sw.WriteLine(TString01);
                    if (!string.IsNullOrEmpty(TString02))
                    {
                        sw.WriteLine(DateTime.Now + " || " + TString02);
                    }

                    sw.WriteLine(LogMsgString);
                    sw.Flush();
                    sw.Close();

                }
                else
                {
                    StreamWriter sw = default(StreamWriter);
                    sw = File.AppendText(path);
                    if (!string.IsNullOrEmpty(TString02))
                    {
                        sw.WriteLine(DateTime.Now + " || " + TString02);
                    }
                    sw.WriteLine(LogMsgString);
                    sw.Flush();
                    sw.Close();
                }
                ReturnMessage = "Log updated.";
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;                
            }
            return ReturnMessage;
        }
    }
}
