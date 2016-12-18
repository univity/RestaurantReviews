using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMI.Logging
{
    /// <summary>
    /// Used to write log entries to the files system.
    /// </summary>
   public class FileLog : LogHandlerBase<FileHandlerConfiguration>, IDisposable
        {
            private const string MAIN_DIVIDER = "*****************************************************************************************";
            private const string SUB_DIVIDER = "-----------------------------------------------------------------------------------------";

            private object _locker = new object();

            public FileLog(LogHandlerBase handler, string key, int tracelevel, string application) : base(handler, key, "FileLogConfig", tracelevel) { }

            public override void HandleError(Exception ex)
            {
                string msg;
                var dt = DateTime.Now;
                var current = ex;

                while (current != null)
                {
                    string stack = current.StackTrace;
                    if (stack != null)
                        stack = stack.Replace(Environment.NewLine, String.Empty);

                    msg = String.Format("{0:yyMMdd:HHmmss} [{1,-25}] {2,-150}Trace: {3}{4}", dt, current.GetType().Name, current.Message, stack, Environment.NewLine);
                    WriteToStartOfFile(Config.ErrorFile, msg);
                    current = current.InnerException;
                }
            }


            public override void HandleTrace(int lvl, string logType, string message)
            {
                WriteToStartOfFile(Config.TraceFile, String.Format("{0:yyMMdd:HHmmss} L:{1} [{3,-10}] {2}", DateTime.Now, lvl, message, logType.ToUpper()));
            }

            private void WriteToStartOfFile(string filename, string newValue)
            {
                var dir = Path.GetDirectoryName(filename);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                var newFileName = String.Format(filename, DateTime.Now);
                if (!File.Exists(newFileName))
                {
                    var fo = File.Create(newFileName);
                    fo.Close();
                    fo.Dispose();
                }

                var ext = Path.GetExtension(newFileName);

                lock (_locker)
                {
                    using (var writer = new System.IO.StreamWriter(newFileName, true))
                    {
                        writer.WriteLine(newValue);
                        writer.Flush();
                        writer.Close();
                    }
                }
            }

            private void AppendException(Exception ex, StringBuilder msg)
            {
                if (msg.Length > 0)
                {
                    msg.AppendLine(MAIN_DIVIDER);
                    msg.AppendLine("Exception: " + ex.GetType().FullName);
                    msg.AppendLine("Message: " + ex.Message);
                    msg.AppendLine("StackTrace:");
                    msg.AppendLine(SUB_DIVIDER);
                    msg.AppendLine(ex.StackTrace);
                }

                if (ex.InnerException != null)
                    AppendException(ex.InnerException, msg);

            }

            #region IDisposable Support
            private bool disposedValue;
            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing) { }
                }
                disposedValue = true;
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }


            #endregion
        }

   public class FileHandlerConfig : ConfigurationSection, ILogHandlerConfig
        {

            public t GetConfig<t>(string key) where t : ConfigurationElement
            {
                return Settings[key] as t;
            }

            [ConfigurationProperty("settings", IsDefaultCollection = true)]
            public FileHandlerCollection Settings
            {
                get
                {
                    return this["settings"] as FileHandlerCollection;
                }
                set
                {
                    this["settings"] = value; ;
                }
            }
        }

   public class FileHandlerConfiguration : ConfigurationElement
        {
            [ConfigurationProperty("key", IsKey = true, IsRequired = true)]
            public string Key
            {
                get
                {
                    return this["key"].ToString();
                }
                set
                {
                    this["key"] = value;
                }
            }
            [ConfigurationProperty("errorfile", IsKey = false, IsRequired = false, DefaultValue = "ErrorLog.txt")]
            public string ErrorFile
            {
                get
                {
                    return this["errorfile"].ToString();
                }
                set
                {
                    this["errorfile"] = value;
                }
            }

            [ConfigurationProperty("tracefile", IsKey = false, IsRequired = false, DefaultValue = "TraceLog.txt")]
            public string TraceFile
            {
                get
                {
                    return this["tracefile"].ToString();
                }
                set
                {
                    this["tracefile"] = value;
                }
            }

            [ConfigurationProperty("requestfile", IsKey = false, IsRequired = false, DefaultValue = "RequestLog.txt")]
            public string RequestFile
            {
                get
                {
                    return this["requestfile"].ToString();
                }
                set
                {
                    this["requestfile"] = value;
                }
            }

            [ConfigurationProperty("responsefile", IsKey = false, IsRequired = false, DefaultValue = "ResponseLog.txt")]
            public string ResponseFile
            {
                get
                {
                    return this["responsefile"].ToString();
                }
                set
                {
                    this["responsefile"] = value;
                }
            }
        }

   [ConfigurationCollection(typeof(FileHandlerConfiguration))]
   public class FileHandlerCollection : ConfigurationElementCollection
        {
            new public FileHandlerConfiguration this[string key]
            {
                get
                {
                    return BaseGet(key) as FileHandlerConfiguration;
                }
                set
                {
                    if (BaseGet(key) != null)
                        BaseRemoveAt(BaseIndexOf(BaseGet(key)));

                    BaseAdd(value);
                }
            }
            protected override ConfigurationElement CreateNewElement()
            {
                return new FileHandlerConfiguration();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return (element as FileHandlerConfiguration).Key;
            }
        }
    
}
