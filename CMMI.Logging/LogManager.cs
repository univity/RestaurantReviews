using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMMI.Logging
{
    /// <summary>
    /// Use to access the configured logging features
    /// </summary>
    public class LogManager : IDisposable
    {
        private LogHandlerBase _handlers = null;

        public LogManager()
        {
            //Loads the logging setup for the web.config 
            if (_handlers == null)
            {
                LogHandlerConfig config = (LogHandlerConfig)ConfigurationManager.GetSection("LogConfig");

                //Load up the handlers
                foreach (LogConfiguration h in config.Handlers)
                {
                    string key = null;
                    try
                    {
                        LogConfiguration settings = (LogConfiguration)h;
                        key = settings.Key;
                        _handlers = GetInstance<LogHandlerBase>(settings.Type, _handlers, settings.SettingKey, settings.TraceLevel);
                    }
                    catch (Exception ex)
                    {
                        throw new ConfigurationErrorsException(String.Format("Unable to load error configuration '{0}'.  See inner exception for details.", key), ex);
                    }
                }
            }
        }

        
        /// <summary>
        /// Log an exception
        /// </summary>
        /// <param name="ex">The exception to be logged</param>
        public void LogException(Exception ex)
        {
            _handlers.LogTrace(1, "ERROR", ex.Message, GetCallingMethod());
            _handlers.LogError(ex);
        }

        /// <summary>
        /// Enters a trace record into the system
        /// </summary>
        /// <param name="level">Logging level to write the trace record</param>
        public void LogTrace(int level)
        {
            _handlers.LogTrace(level, "info", null, GetCallingMethod());
        }

        /// <summary>
        /// Enters a trace record into the system
        /// </summary>
        /// <param name="level">Logging level to write the trace record</param>
        /// <param name="message">Message to right in the trace record</param>
        public void LogTrace(int level, string message)
        {
            _handlers.LogTrace(level, "info", message, GetCallingMethod());
        }

        /// <summary>
        /// Enters a trace record into the system
        /// </summary>
        /// <param name="level">Logging level to write the trace record</param>
        /// <param name="logType">Trace type to be stored</param>
        /// <param name="message">Message to right in the trace record</param>
        public void LogTrace(int level, string logType, string message)
        {
            _handlers.LogTrace(level, logType, message, GetCallingMethod());
        }

        /// <summary>
        /// Finds the calling method in the stack trace
        /// </summary>
        /// <returns>The name of the method</returns>
        private string GetCallingMethod()
        {
            try
            {
                MethodBase method = (new StackFrame(2)).GetMethod();
                if (method == null)
                    method = (new StackFrame(1)).GetMethod();

                return String.Format("{0}.{1}", method.DeclaringType.Name, method.Name);
            }
            catch (Exception)
            {
                return "Caller Not Found";
            }
        }

        /// <summary>
        /// Create an instance of an object based on a type string
        /// </summary>
        /// <typeparam name="t">Type to be returned</typeparam>
        /// <param name="typestring">Type string to create</param>
        /// <param name="values">Paramater values need to initialize the object</param>
        /// <returns>New instance of type T</returns>
        private t GetInstance<t>(string typestring, params object[] values)
        {
            var tp = GetTypeWithAssignableCheck<t>(typestring);
            return (t)Activator.CreateInstance(tp, values);
        }

        /// <summary>
        /// Checks if the type to be created is also a assignable as the type to be returned.
        /// </summary>
        /// <typeparam name="t">Type to be returned</typeparam>
        /// <param name="typestring">Type to be instantiated</param>
        /// <returns>A new instance of type t</returns>
        private Type GetTypeWithAssignableCheck<t>(string typestring)
        {
            var tp = Type.GetType(typestring);

            if (tp == null)
                throw new InvalidCastException(String.Format("Unable to convert '{0}' to a type in the system.", typestring));

            if (!typeof(t).IsAssignableFrom(tp))
                throw new InvalidCastException(String.Format("Unable to cast type '{0}' to a type '{1}'.", tp.FullName, typeof(t).FullName));

            return tp;
        }

        /// <summary>
        /// Disposes all of the insantiated log handlers.
        /// </summary>
        public void Dispose()
        {
            if (_handlers != null)
                _handlers.ChainDispose();
        }
    }
}
