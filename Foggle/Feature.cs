using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

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

		    var attr = typeof(T).GetCustomAttributes(false);

		    foreach (var a in attr)
		    {
				if (a.GetType() == typeof(FoggleByHostnameAttribute))
				{
					return IsEnabledByHostname(appSettingsKey);
				}
		    }

			return IsEnabledInConfig<T>(appSettingsKey);
		}

		private static bool IsEnabledInConfig<T>(string appSettingsKey) where T : FoggleFeature
		{
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

		private static bool IsEnabledByHostname(string appSettingsKey)
		{
			var hostnames = configurationWrapper.GetApplicationSetting($"{appSettingsKey}.Hostnames");
			var currentHostname = configurationWrapper.GetCurrentHostname();

			if (hostnames == null)
			{
				return false;
			}

			if (currentHostname == null)
			{
				throw new FoggleException("Unable to get the hostname for current machine");
			}

			var enabledHosts = hostnames.Split(new []{'|'}, StringSplitOptions.RemoveEmptyEntries).ToList();

			if (currentHostname.MatchesAnyOfThe(enabledHosts))
			{
				return true;
			}
			return false;
		}

		private static bool MatchesAnyOfThe(this string hostname, List<string> enabledHosts)
		{
			foreach (var h in enabledHosts)
			{
				if (hostname == h)
				{
					return true;
				}
				Regex r = new Regex($"^{h.Replace("*", ".+")}$");
				if (r.IsMatch(hostname))
				{
					return true;
				}
			}
			return false;
		}

	}
}