using System;
using System.Configuration;

namespace Foggle
{
	internal class ConfigurationWrapper : IConfigWrapper
	{
		public string GetApplicationSetting(string key)
		{
			try
			{
				return ConfigurationManager.AppSettings[key];
			}
			catch (Exception ex)
			{
				return string.Empty;
			}
		}

		public string GetCurrentHostname()
		{
			try
			{
				return System.Net.Dns.GetHostName();
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}