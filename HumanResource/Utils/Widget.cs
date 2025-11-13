namespace HumanResource.Utils
{
    public class Widget
    {


        public static void LogFileWrite(string MethodName, string message)
        {
            System.IO.FileStream fileStream = null;
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string logFilePath = "D:\\HR-byaleem\\";

                logFilePath = logFilePath + MethodName + "-ProgramLog" + "-" + DateTime.Today.ToString("yyyyMMdd") + "." + "txt";

                if (logFilePath.Equals("")) return;

                // Create the Log file directory if it does not exists
                System.IO.DirectoryInfo logDirInfo = null;
                System.IO.FileInfo logFileInfo = new System.IO.FileInfo(logFilePath);
                logDirInfo = new System.IO.DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                // Create the Log file directory if it does not exists

                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new System.IO.FileStream(logFilePath, System.IO.FileMode.Append);
                }
                streamWriter = new System.IO.StreamWriter(fileStream);
                streamWriter.WriteLine(message+ DateTime.Now);
            }
            catch (Exception ex)
            {
                 ex.StackTrace.ToString();
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }
        }
    }
}
