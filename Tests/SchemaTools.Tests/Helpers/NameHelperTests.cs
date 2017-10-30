using FluentAssertions;
using Telegram4Net.SchemaTools;
using Telegram4Net.SchemaTools.Helpers;
using Xunit;

namespace SchemaTools.Tests.Helpers
{
    public class NameHelperTests
    {
        [Theory]
        [InlineData("Stickers")]
        public void GetNameSpace_TypeDoesNotContainDot_ReturnsDomainNameFolder(string type)
        {
            string result = NameHelper.GetNameSpace(type);

            result.Should().Be(Constants.FullDomainNameFolder);
        }

        [Theory]
        [InlineData("messages.Stickers")]
        public void GetNameSpace_TypeContainsDot_ReturnsDomainNameFolder(string type)
        {
            string expectedResult = $"{Constants.FullDomainNameFolder}.Messages";

            string result = NameHelper.GetNameSpace(type);

            result.Should().Be(expectedResult);
        }
    }
}