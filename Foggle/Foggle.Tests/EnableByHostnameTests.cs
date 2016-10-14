using System;
using Moq;
using Xunit;
using Should;

namespace Foggle
{
	public class EnableByHostnameTests
	{
		[Fact]
		public void IsEnabled_ClassMarkedWithFoggleByHostName_TrysToGetListOfHostname()
		{
			var mockConfig = new Mock<IConfigWrapper>();

			Feature.configurationWrapper = mockConfig.Object;
			Feature.IsEnabled<TestFeature>().ShouldBeFalse();

			mockConfig.Verify(x => x.GetApplicationSetting(It.Is<string>(s => s.EndsWith("Hostnames"))));
		}

		[FoggleByHostname]
		class TestFeature : FoggleFeature
		{

		}
	}
}
