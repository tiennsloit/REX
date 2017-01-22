using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core
{
    public static class StringHelper
    {
        public static T GetAppSettingValueOrDefault<T>(string key, T defaultValue)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
            }

            return defaultValue;
        }
    }
}
