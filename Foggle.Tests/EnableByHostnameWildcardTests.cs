
using System;
using Moq;
using Xunit;
using Should;

namespace Foggle
{
	[Collection("Feature Tests")]
	public class EnableByHostnameWildcardTests
	{
		[Fact]
		public void IsEnabledByHostName_WildcardAtStart_ReturnsTrue()
		{
			var mockConfig = new Mock<IConfigWrapper>();
			mockConfig.Setup(s => s.GetCurrentHostname()).Returns("MY-COMPUTER");
			mockConfig.Setup(x => x.GetApplicationSetting(It.Is<string>(s => s.EndsWith("Hostnames")))).Returns("*PUTER");
			Feature.configurationWrapper = mockConfig.Object;

			Feature.IsEnabled<TestHostnameFeature>().ShouldBeTrue();
		}

		[Fact]
		public void IsEnabledByHostName_WildcardAtEnd_ReturnsTrue()
		{
			var mockConfig = new Mock<IConfigWrapper>();
			mockConfig.Setup(s => s.GetCurrentHostname()).Returns("MY-COMPUTER");
			mockConfig.Setup(x => x.GetApplicationSetting(It.Is<string>(s => s.EndsWith("Hostnames")))).Returns("MY-COM*");
			Feature.configurationWrapper = mockConfig.Object;

			Feature.IsEnabled<TestHostnameFeature>().ShouldBeTrue();
		}

		[Fact]
		public void IsEnabledByHostName_WildcardWithin_ReturnsTrue()
		{
			var mockConfig = new Mock<IConfigWrapper>();
			mockConfig.Setup(s => s.GetCurrentHostname()).Returns("MY-COMPUTER");
			mockConfig.Setup(x => x.GetApplicationSetting(It.Is<string>(s => s.EndsWith("Hostnames")))).Returns("MY-CO*TER");
			Feature.configurationWrapper = mockConfig.Object;

			Feature.IsEnabled<TestHostnameFeature>().ShouldBeTrue();
		}

		[Fact]
		public void IsEnabledByHostName_MultipleWildcardWithin_ReturnsTrue()
		{
			var mockConfig = new Mock<IConfigWrapper>();
			mockConfig.Setup(s => s.GetCurrentHostname()).Returns("MY-COMPUTER");
			mockConfig.Setup(x => x.GetApplicationSetting(It.Is<string>(s => s.EndsWith("Hostnames")))).Returns("MY*OMPU*");
			Feature.configurationWrapper = mockConfig.Object;

			Feature.IsEnabled<TestHostnameFeature>().ShouldBeTrue();
		}

		[Fact]
		public void IsEnabledByHostName_WildcardHostnameInPipeDelimitedList_ReturnsTrue()
		{
			var mockConfig = new Mock<IConfigWrapper>();
			mockConfig.Setup(s => s.GetCurrentHostname()).Returns("MY-COMPUTER");
			mockConfig.Setup(x => x.GetApplicationSetting(It.Is<string>(s => s.EndsWith("Hostnames")))).Returns("STEVEPC|MY-*UTER|DAVEPC");
			Feature.configurationWrapper = mockConfig.Object;

			Feature.IsEnabled<TestHostnameFeature>().ShouldBeTrue();
		}

		[FoggleByHostname]
		class TestHostnameFeature : FoggleFeature
		{

		}
	}
}
