using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace QuartzAdmin.config
{
    public class Reader
    {
        //TODO:  Add in config section handlers for encryption
        public static string GetSetting(string settingName)
        {
            if (System.Configuration.ConfigurationManager.AppSettings[settingName] == null)
                return "";
            else
                return System.Configuration.ConfigurationManager.AppSettings[settingName];
        }

        //TODO:  Add in config section handlers for encryption
        public static System.Collections.Specialized.NameValueCollection GetQuartzConfig()
        {
            System.Collections.Specialized.NameValueCollection props = new System.Collections.Specialized.NameValueCollection();

            object oCAPSSection = System.Configuration.ConfigurationManager.AppSettings;
            foreach (string key in System.Configuration.ConfigurationManager.AppSettings.Keys)
            {
                if (key.StartsWith("quartz"))
                {
                    props.Add(key, System.Configuration.ConfigurationManager.AppSettings[key]);
                }
            }
            
            /*
            if (oCAPSSection is CAPSConfigSection)
            {
                ConfigurationElementCollection cec = ((CAPSConfigSection)oCAPSSection).ElementCollection;
                foreach (CAPSConfigSection.CAPSConfigElement el in cec)
                {
                    if (el.Key.StartsWith("quartz"))
                    {
                        props.Add(el.Key, el.Value);
                    }


                }

            }
            else if (oCAPSSection is CAPSEncryptedConfigSection)
            {
                ConfigurationElementCollection cec = ((CAPSEncryptedConfigSection)oCAPSSection).ElementCollection;
                foreach (CAPSEncryptedConfigElement el in cec)
                {
                    if (el.Key.StartsWith("quartz"))
                    {
                        props.Add(el.Key, el.Value);
                    }


                }
            }
             * */

            return props;
        }

    }
}
