using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Russ.Logger
{
    public class Log 
    {
        public static void Close()
        {
            abort = true;

            mre.Set();
            mre.Dispose();
        }
        private Log()
        {

            

        }
        private Log(Type classLogger)
        {
            ClassLogger = classLogger;
        }
        Type ClassLogger = null;
       
        static LogLevels LogLevel = LogLevels.Fatal;

        static string LogFile = null;
        static StreamWriter dataStream = null;
        public static Log GetLogger(Type classLogger)
        {
            
            return new Log(classLogger);
            

        }
       
        public static void InitializeLogging(LogLevels level, string filePath, bool forAppend)
        {
#if NOLOG
#else


            try
            {

                LogFile = filePath.ToUpperInvariant().Replace("%USERPROFILE%", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

                LogLevel = level;

                
                dataStream = new System.IO.StreamWriter(LogFile, forAppend); // new FileStream(LogFile, mode, FileAccess.Write);
                loggingThread.Start();
            }
            catch { }
#endif
        }
        public static void Flush()
        {
            if (dataStream != null)
            {
                dataStream.Flush();
            }
        }
        private void LogData(string message, LogLevels level, Exception exception)
        {
            if (LogLevel <= level)
            {
                if (exception == null)
                {
                    LogData(string.Format("{0} - {1} - {2} - {3}", DateTime.Now.ToString("MM/dd/yyyy HH:nn:ss.fffff"), ClassLogger.ToString(), level.ToString(), message));
                }
                else
                {
                    LogData(string.Format("{0} - {1} - {2} - {3}::\r\n{4}", DateTime.Now.ToString("MM/dd/yyyy HH:nn:ss.fffff"), ClassLogger.ToString(), level.ToString(), message, exception.ToString()));
                }
            }
        }
      
        static System.Threading.Thread loggingThread = new Thread(new ThreadStart(WriteProcessing));
        private static ManualResetEvent mre = new ManualResetEvent(false);
        private static void LogData(string message)
        {
#if NOLOG
#else

            Messages.Enqueue(message);
            mre.Set();
#endif
        }

        static bool abort = false;
        private static void WriteProcessing()
        {
            while (!abort)
            {
                mre.WaitOne();
                while (Messages.Count > 0)
                {
                    
                    dataStream.WriteLine(Messages.Dequeue());
                }
                if (!abort)
                {
                    mre.Reset();
                }
            }
        }
        private static Queue<string> Messages = new Queue<string>();

        public void Debug(string message)
        {
            LogData(message, LogLevels.Debug, null);
        }

        public void DebugFormat(string message, params object[] parms)
        {
            LogData(string.Format(message, parms), LogLevels.Debug, null);
        }
        
        public void Info(string message)
        {
            LogData(message, LogLevels.Info, null);
        }
        public void InfoFormat(string message, params object[] parms)
        {
            LogData(string.Format(message, parms), LogLevels.Info, null);
        }
        
        public void Warn(string message)
        {
            LogData(message, LogLevels.Warn, null);
        }
        public void WarnFormat(string message, params object[] parms)
        {
            LogData(string.Format(message, parms), LogLevels.Warn , null);
        }
        
        public void Error(string message)
        {
            LogData(message, LogLevels.Error, null);
        }
        public void ErrorFormat(string message, params object[] parms)
        {
            LogData(string.Format(message, parms), LogLevels.Error, null);
        }
        public void Fatal(string message)
        {
            LogData(message, LogLevels.Fatal, null);
        }

        public void FatalFormat(string message, params object[] parms)
        {
            LogData(string.Format(message, parms), LogLevels.Fatal, null);
        }

        public void Debug(string message, Exception exception)
        {
            LogData(message, LogLevels.Debug, exception);
        }

        public void Info(string message, Exception exception)
        {
            LogData(message, LogLevels.Info, exception);
        }
        public void Warn(string message, Exception exception)
        {
            LogData(message, LogLevels.Warn, exception);
        }
        public void Error(string message, Exception exception)
        {
            LogData(message, LogLevels.Error, exception);
        }
        public void Fatal(string message, Exception exception)
        {
            LogData(message, LogLevels.Fatal, exception);
        }

        /*
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        bool isDisposed =false;
        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    abort = true;
                    if (mre != null)
                    {
                        mre.Set();
                        mre.Dispose();
                    }
                }
            }
        }
         * */
    }
    public enum LogLevels
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }
}

