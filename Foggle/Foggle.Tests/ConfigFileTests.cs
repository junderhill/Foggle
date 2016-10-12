using System;
using Moq;
using Xunit;
using Should;

namespace Foggle
{
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
	}

	public class TestFeature : FoggleFeature
	{
	}
}
