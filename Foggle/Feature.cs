using System.Configuration;

namespace Foggle
{
    public static class Feature
    {
		private static IConfigWrapper _configurationWrapper;
		internal static IConfigWrapper configurationWrapper
		{
			get
			{
				if (_configurationWrapper == null)
				{
					_configurationWrapper = new ConfigurationWrapper();
				}
				return _configurationWrapper;
			}
			set
			{
				_configurationWrapper = value;
			}
		}

		private const string Prefix = "Foggle.";
        public static bool IsEnabled<T>() where T : FoggleFeature
        {
            var classname = typeof(T).Name;
            var appSettingsKey = $"{Prefix}{classname}";
			var configsetting = configurationWrapper.GetApplicationSetting(appSettingsKey);
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