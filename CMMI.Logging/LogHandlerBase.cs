using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Claims;

namespace CMMI.Logging
{
    /// <summary>
    /// Base class used for the log handlers
    /// </summary>
    public abstract class LogHandlerBase
    {
        private LogHandlerBase _handler;
        private LogHandlerBase _parent;
        private int _tracelevel = 1;

        /// <summary>
        /// Constructor for the log handler
        /// </summary>
        /// <param name="handler">The handler chain to be tied to.</param>
        /// <param name="tracelevel">The lowestr level trave to be logged by this handler</param>
        public LogHandlerBase(LogHandlerBase handler, int tracelevel)
        {
            _tracelevel = tracelevel;
            _handler = handler;
            _parent = this;
            SetParent(ref _parent);
        }

        /// <summary>
        /// abstract method used to handle logging the exception
        /// </summary>
        /// <param name="ex">The exception to be logged</param>
        public abstract void HandleError(Exception ex);

        /// <summary>
        /// Abstract method used to handle logging the trace 
        /// </summary>
        /// <param name="lvl">The trace level of the event</param>
        /// <param name="logType">The log type of the event</param>
        /// <param name="message">The message of the event to be stored.</param>
        public abstract void HandleTrace(int lvl, string logType, string message);

        /// <summary>
        /// Sets the parent handler
        /// </summary>
        /// <param name="parent">The parent handler</param>
        private void SetParent(ref LogHandlerBase parent)
        {
            _parent = parent;
            if(_handler != null) _handler.SetParent(ref _parent);
            
        }

        /// <summary>
        /// Starts logging an exception
        /// </summary>
        /// <param name="ex">Exception to log</param>
        public void LogError(Exception ex)
        {
            LogError(ex, false);
        }

        /// <summary>
        /// Starts logging the trace event
        /// </summary>
        /// <param name="lvl">The trace level of the event</param>
        /// <param name="logType">The log type of the event</param>
        /// <param name="message">The message of the event to be stored.</param>
        /// <param name="method">The calling method</param>
        public void LogTrace(int level, string logType, string message, string method)
        {
            if( level <= _tracelevel)
                HandleTrace(level, logType, message);
            
            if(_handler != null) _handler.LogTrace(level, logType, message, method);
        }

        /// <summary>
        /// Starts logging an exception
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <param name="isHandler">Did a log handler throw this exception</param>
        private void LogError(Exception ex, bool isHandler)
        {
            try
            {
                HandleError(ex);
            }
            catch(Exception){}
            finally
            {
                if(_handler != null) _handler.LogError(ex, isHandler);
            }
        }

        /// <summary>
        /// Trigger the whole handler chain to dispose itself
        /// </summary>
        public void ChainDispose()
        {
            if(_handler != null)
                _handler.ChainDispose();

            if(this is IDisposable)
                ((IDisposable)this).Dispose();

        }
    }

    /// <summary>
    /// Base class used for the log handlers
    /// </summary>
    /// <typeparam name="t">Type of the log settings to be loaded.</typeparam>
    public abstract class LogHandlerBase<t> : LogHandlerBase  where t : ConfigurationElement
    {
        private string _configKey;
        private string _sectionName;

        /// <summary>
        /// Constructor for the log handler
        /// </summary>
        /// <param name="handler">The handler chain to be tied to.</param>
        /// <param name="configkey">The configuration key for the handlers settings</param>
        /// <param name="sectioname">The section used to hold the settings.</param>
        /// <param name="tracelevel">The lowestr level trave to be logged by this handler</param>
        public LogHandlerBase(LogHandlerBase handler, string configkey, string sectioname, int tracelevel) : base(handler, tracelevel)
        {
            _configKey = configkey;
            _sectionName = sectioname;
        }

        /// <summary>
        /// The configuration settings for the handler
        /// </summary>
        protected t Config
        {
            get
            {
                var c = (ILogHandlerConfig)ConfigurationManager.GetSection(_sectionName);
                return c.GetConfig<t>(_configKey);
            }
        }
    }

   
}
