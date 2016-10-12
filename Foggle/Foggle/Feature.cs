using System.Configuration;

namespace Foggle
{
    public static class Feature
    {
        private const string Prefix = "Foggle.";
        public static bool IsEnabled<T>() where T : FoggleFeature
        {
            var classname = typeof(T).Name;
            var appSettingsKey = $"{Prefix}{classname}";
            var configsetting = ConfigurationManager.AppSettings[appSettingsKey];
            if (configsetting != null)
            {
                var enabled = false;
                if (bool.TryParse(configsetting, out enabled))
                {
                    return enabled;
                }
            }
            return false;
        }
        
    }
}