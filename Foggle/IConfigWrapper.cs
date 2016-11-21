namespace Foggle
{
	internal interface IConfigWrapper
	{
		string GetApplicationSetting(string key);
		string GetCurrentHostname();
	}
}