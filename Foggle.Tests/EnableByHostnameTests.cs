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

		[Fact]
		public void IsEnabled_ClassMarkedWithFoggleByHostName_GetsCurrentHostname()
		{
			var mockConfig = new Mock<IConfigWrapper>();

			Feature.configurationWrapper = mockConfig.Object;
			Feature.IsEnabled<TestFeature>();

			mockConfig.Verify(x => x.GetCurrentHostname(), Times.Once);
		}

		[Fact]
		public void IsEnabledByHostName_HostnameInList_ReturnsTrue()
		{
			var mockConfig = new Mock<IConfigWrapper>();

			Feature.configurationWrapper = mockConfig.Object;
			Feature.IsEnabled<TestFeature>();

			mockConfig.Verify(x => x.GetCurrentHostname(), Times.Once);
		}




		[FoggleByHostname]
		class TestFeature : FoggleFeature
		{

		}
	}
}
