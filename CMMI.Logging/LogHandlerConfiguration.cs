using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CMMI.Logging
{
    public class LogHandlerConfig : System.Configuration.ConfigurationSection 
    {
        [ConfigurationProperty("handlers", IsDefaultCollection=true)] 
        public LogHandlerCollection Handlers
        {
            get
            {
                return (LogHandlerCollection)this["handlers"];
            }
            set
            {
                this["handlers"] = value;
            }
        }
    }

    public class LogConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("key", IsKey=true, IsRequired=true)]
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

        [ConfigurationProperty("settingkey", IsKey=false, IsRequired=false, DefaultValue=null)]
        public string SettingKey
        {
            get
            {
                return this["settingkey"].ToString();
            }
            set
            {
                this["settingkey"] = value;
            }
        }

        [ConfigurationProperty("tracelevel", IsKey = false, IsRequired = false, DefaultValue = 0)]
        public int TraceLevel
        {
            get
            {
                return (int)this["tracelevel"];
            }
            set
            {
                this["tracelevel"] = value;
            }
        }

        [ConfigurationProperty("type", IsKey=false, IsRequired=true)]
        public string Type
        {
            get
            {
                return this["type"].ToString();
            }
            set
            {
                this["type"] = value;
            }
        }
    }

    [ConfigurationCollection(typeof(LogConfiguration))]
    public class LogHandlerCollection : ConfigurationElementCollection 
    {    
        new public LogConfiguration this[string key]
        {
            get
            {
                return (LogConfiguration)base.BaseGet(key);
            }
            set
            {
                if(BaseGet(key)!= null)
                    BaseRemoveAt(BaseIndexOf(BaseGet(key)));

                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new LogConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LogConfiguration)element).Key;
        }
    }
}
