using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMI.Logging
{
    /// <summary>
    /// The log handlers interface for loading the configuration settings.
    /// </summary>
    public interface ILogHandlerConfig
    {
        /// <summary>
        /// Retrieves the log handlers configuration settings.
        /// </summary>
        /// <typeparam name="t">The configuration element type</typeparam>
        /// <param name="key">The key for the configuration element</param>
        /// <returns>The specified configuration element</returns>
        t GetConfig<t>(string key) where t : System.Configuration.ConfigurationElement;
    }
}
