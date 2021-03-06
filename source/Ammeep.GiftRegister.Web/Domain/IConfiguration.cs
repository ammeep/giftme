using System;
using System.Configuration;

namespace Ammeep.GiftRegister.Web.Domain
{
    public interface IConfiguration
    {
        string GiftmeConnectionString { get; }
        int RegistryPageSize { get; }
        int MinimumPasswordLength { get; }
        int AuthenticationExipryMinutes { get; }
        string AuthenticatedUserCookieName { get; }
    }

    public class Configuration : IConfiguration
    {

        private static string GetApplicationConfigurationValue(string key)
        {
            return key != null ? ConfigurationManager.AppSettings.Get(key) : string.Empty;
        }

        private static T GetApplicationConfigurationValue<T>(string key, bool required)
        {
            try
            {
                var stringValue = GetApplicationConfigurationValue(key);
                return (T) Convert.ChangeType(stringValue, typeof (T));
            }
            catch(Exception exception)
            {
               if(!required)
               {
                   return default(T);
               }
   
               throw new ConfigurationErrorsException(string.Format("Could not find a configuration value of type {0} for the key {1}", typeof(T), key),exception);
            }    
        }


        private static string GetConnectionStringValue(string key)
        {
            if (key != null)
            {
                var connectionString = ConfigurationManager.ConnectionStrings[key];
                return connectionString !=null  ? connectionString.ConnectionString : string.Empty;
            }
            return string.Empty;
        }

        public string GiftmeConnectionString
        {
            get { return GetConnectionStringValue("giftmedataconnectionstring"); }
        }

        public int RegistryPageSize
        {
            get { return GetApplicationConfigurationValue<int>("RegistryPageSize",false); }
        }

        public int MinimumPasswordLength
        {
            get { return GetApplicationConfigurationValue<int>("MinimumPasswordLength", true); }
        }

        public int AuthenticationExipryMinutes
        {
            get { return GetApplicationConfigurationValue<int>("AuthenticationExipryMinutes", true); }
        }

        public string AuthenticatedUserCookieName
        {
            get { return GetApplicationConfigurationValue<string>("AuthenticatedUserCookieName", true); }
        }
    }


}