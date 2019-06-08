using System;
using System.Configuration;

namespace Utility
{
    public class ConfigUtility
    {
        public static string GetStringFromConfig(string keyName)
        {
            string sValue = string.Empty;
            string configKeyValue = ConfigurationManager.AppSettings[keyName];
            if (!string.IsNullOrWhiteSpace(configKeyValue))
            {
                sValue = configKeyValue;
            }

            return sValue;
        }
        public static string GetStringFromConfigOrDefault(string keyName, string defaultValue)
        {
            string sValue = defaultValue;
            string configKeyValue = ConfigurationManager.AppSettings[keyName];
            if (!string.IsNullOrWhiteSpace(configKeyValue))
            {
                sValue = configKeyValue;
            }
            return sValue;
        }
        public static int GetIntFromConfig(string keyName)
        {
            int nValue = 0;
            string configKeyValue = ConfigurationManager.AppSettings[keyName];
            int.TryParse(configKeyValue, out nValue);
            return nValue;
        }

        public static long GetLongFromConfig(string keyName)
        {
            long nValue = 0;
            string configKeyValue = ConfigurationManager.AppSettings[keyName];
            long.TryParse(configKeyValue, out nValue);
            return nValue;
        }

        public static Decimal GetDecimalFromConfig(string keyName)
        {
            Decimal nValue = 0M;
            string configKeyValue = ConfigurationManager.AppSettings[keyName];
            decimal.TryParse(configKeyValue, out nValue);
            return nValue;
        }
       
        public static double GetDoubleFromConfig(string keyName)
        {
            double nValue = 0D;
            string configKeyValue = ConfigurationManager.AppSettings[keyName];
            double.TryParse(configKeyValue, out nValue);
            return nValue;
        }

        public static bool GetBoolFromConfig(string keyName)
        {
            bool value = false;
            string configKeyValue = ConfigurationManager.AppSettings[keyName];
            bool.TryParse(configKeyValue, out value);
            return value;
        }

        public static int GetIntFromConfigOrDefault(string keyName, int defaultValue)
        {
            int nValue = 0;
            string configKeyValue = ConfigurationManager.AppSettings[keyName];
            int.TryParse(configKeyValue, out nValue);
            nValue = nValue == 0 ? defaultValue : nValue;
            return nValue;
        }

        public static DateTime GetDateTimeFromConfig(string keyName)
        {
            DateTime value;
            string configKeyValue = ConfigurationManager.AppSettings[keyName];
            DateTime.TryParse(configKeyValue, out value);
            return value;
        }

        public static byte GetByteFromConfig(string keyName)
        {
            return GetByteFromConfigOrDefault(keyName, 0);
        }
        public static byte GetByteFromConfigOrDefault(string keyName, byte defaultValue)
        {
            byte nValue = 0;
            string configKeyValue = ConfigurationManager.AppSettings[keyName];
            byte.TryParse(configKeyValue, out nValue);
            nValue = nValue == 0 ? defaultValue : nValue;
            return nValue;
        }

    }
}