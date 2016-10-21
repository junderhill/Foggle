using System;
using Moq;
using Xunit;
using Should;

namespace Foggle
{
	[Collection("Feature Tests")]
	public class EnableByHostnameTests
	{
		[Fact]
		public void IsEnabled_ClassMarkedWithFoggleByHostName_TrysToGetListOfHostname()
		{
			var mockConfig = new Mock<IConfigWrapper>();

			Feature.configurationWrapper = mockConfig.Object;
			Feature.IsEnabled<TestHostnameFeature>();

			mockConfig.Verify(x => x.GetApplicationSetting(It.Is<string>(s => s.EndsWith("Hostnames"))));
		}

		[Fact]
		public void IsEnabled_ClassMarkedWithFoggleByHostName_GetsCurrentHostname()
		{
			var mockConfig = new Mock<IConfigWrapper>();

			Feature.configurationWrapper = mockConfig.Object;
			Feature.IsEnabled<TestHostnameFeature>();

			mockConfig.Verify(x => x.GetCurrentHostname(), Times.Once);
		}

		[Fact]
		public void IsEnabledByHostName_OnlyHostnameInList_ReturnsTrue()
		{
			var mockConfig = new Mock<IConfigWrapper>();
			mockConfig.Setup(s => s.GetCurrentHostname()).Returns("MY-COMPUTER");
			mockConfig.Setup(x => x.GetApplicationSetting(It.Is<string>(s => s.EndsWith("Hostnames")))).Returns("MY-COMPUTER");
			Feature.configurationWrapper = mockConfig.Object;

			Feature.IsEnabled<TestHostnameFeature>().ShouldBeTrue();
		}

		[Fact]
		public void IsEnabledByHostName_OneDifferentHostnameInList_ReturnsFalse()
		{
			var mockConfig = new Mock<IConfigWrapper>();
			mockConfig.Setup(s => s.GetCurrentHostname()).Returns("MY-COMPUTER");
			mockConfig.Setup(x => x.GetApplicationSetting(It.Is<string>(s => s.EndsWith("Hostnames")))).Returns("DAVEPC");
			Feature.configurationWrapper = mockConfig.Object;

			Feature.IsEnabled<TestHostnameFeature>().ShouldBeFalse();
		}

		[FoggleByHostname]
		class TestHostnameFeature : FoggleFeature
		{

		}
	}
}
