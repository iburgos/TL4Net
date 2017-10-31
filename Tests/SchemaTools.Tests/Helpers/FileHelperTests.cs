using FluentAssertions;
using Telegram4Net.SchemaTools.Helpers;
using Xunit;

namespace SchemaTools.Tests.Helpers
{
    public class FileHelperTests
    {
        [Theory]
        [InlineData("Stickers")]
        public void GetFolderName_TypeDoesNotContainDot_ReturnsBaseFolderName(string type)
        {
            string expectedResult = $"{FileHelper.RootFolder}\\{FileHelper.DomainFolder}";

            string result = FileHelper.GetFolderName(type);

            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("messages.Stickers")]
        public void GetFolderName_TypeContainsDot_ReturnsDomainNameFolder(string type)
        {
            string expectedResult = $"{FileHelper.RootFolder}\\{FileHelper.DomainFolder}\\Messages";

            string result = FileHelper.GetFolderName(type);

            result.Should().Be(expectedResult);
        }
    }
}