using System;
using Moq;
using Xunit;
using Should;

namespace Foggle
{
	[Collection("Feature Tests")]
	public class ConfigFileTests
	{
		[Fact]
		public void IsEnabled_NoMatchingConfigSetting_ReturnsFalse()
		{
			var mockConfig = new Mock<IConfigWrapper>();
			mockConfig.Setup(x => x.GetApplicationSetting(It.IsAny<string>())).Returns<string>(null);

			Feature.configurationWrapper = mockConfig.Object;

			Feature.IsEnabled<TestFeature>().ShouldBeFalse();
		}

		[Fact]
		public void IsEnabled_MatchingConfigSettingSetToFalse_ReturnsFalse()
		{
			var mockConfig = new Mock<IConfigWrapper>();
			mockConfig.Setup(x => x.GetApplicationSetting(It.IsAny<string>())).Returns("false");

			Feature.configurationWrapper = mockConfig.Object;

			Feature.IsEnabled<TestFeature>().ShouldBeFalse();
		}

		[Fact]
		public void IsEnabled_MatchingConfigSettingSetToTrue_ReturnsTrue()
		{
			var mockConfig = new Mock<IConfigWrapper>();
			mockConfig.Setup(x => x.GetApplicationSetting(It.IsAny<string>())).Returns("true");

			Feature.configurationWrapper = mockConfig.Object;

			Feature.IsEnabled<TestFeature>().ShouldBeTrue();
		}

		class TestFeature : FoggleFeature
		{
		}

	}

	
}
