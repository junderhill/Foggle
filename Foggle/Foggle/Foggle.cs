using System.Configuration;

namespace Foggle
{
    public static class Foggle
    {
        private const string Prefix = "Foggle.";
        public static bool IsEnabled(this Feature self)
        {
            var classname = self.GetType().Name;

            var appSettingsKey = $"{Prefix}{classname}";

            var configsetting = ConfigurationManager.AppSettings[appSettingsKey];
            if (configsetting == null) return false;

            var enabled = false;
            if (bool.TryParse(configsetting, out enabled))
            {
                return enabled;
            }
            return false;
        }
        
    }
}