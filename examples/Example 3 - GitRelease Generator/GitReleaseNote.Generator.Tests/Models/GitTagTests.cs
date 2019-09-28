using GitReleaseNote.Generator.Models;
using Xunit;
namespace GitReleaseNote.Generator.Tests.Models
{
    public class GitTagTests
    {
        [Theory]
        [InlineData("1", 1, 0, 0, 0, "", "")]
        [InlineData("1.2", 1, 2, 0, 0, "", "")]
        [InlineData("1.2.3", 1, 2, 3, 0, "", "")]
        [InlineData("1.2.3.1", 1, 2, 3, 1, "", "")]
        [InlineData("1-prerelease-50", 1, 0, 0, 0, "", "-prerelease-50")]
        [InlineData("1.2-prerelease-50", 1, 2, 0, 0, "", "-prerelease-50")]
        [InlineData("1.2.3-prerelease-50", 1, 2, 3, 0, "", "-prerelease-50")]
        [InlineData("1.2.3.1-prerelease-50", 1, 2, 3, 1, "", "-prerelease-50")]
        [InlineData("v1", 1, 0, 0, 0, "v", "")]
        [InlineData("v1.2", 1, 2, 0, 0, "v", "")]
        [InlineData("v1.2.3", 1, 2, 3, 0, "v", "")]
        [InlineData("v1.2.3.1", 1, 2, 3, 1, "v", "")]
        [InlineData("v1-prerelease-50", 1, 0, 0, 0, "v", "-prerelease-50")]
        [InlineData("v1.2-prerelease-50", 1, 2, 0, 0, "v", "-prerelease-50")]
        [InlineData("v1.2.3-prerelease-50", 1, 2, 3, 0, "v", "-prerelease-50")]
        [InlineData("v1.2.3.1-prerelease-50", 1, 2, 3, 1, "v", "-prerelease-50")]
        [InlineData("version-1-prerelease-50", 1, 0, 0, 0, "version-", "-prerelease-50")]
        [InlineData("version-1.2-prerelease-50", 1, 2, 0, 0, "version-", "-prerelease-50")]
        [InlineData("version-1.2.3-prerelease-50", 1, 2, 3, 0, "version-", "-prerelease-50")]
        [InlineData("version-1.2.3.1-prerelease-50", 1, 2, 3, 1, "version-", "-prerelease-50")]
        public void Parse_WithTagInformation_ExpectedTheCorrectTagObject(
            string inputValue,
            int? expectedMajor,
            int? expectedMinor,
            int? expectedPatch,
            int? expectedFix,
            string expectedPrefix,
            string expectedSuffix)
        {
            // Act
            var tagValue = GitTag.Parse(inputValue);

            // Assert
            Assert.Equal(expectedMajor, tagValue.Major);
            Assert.Equal(expectedMinor, tagValue.Minor);
            Assert.Equal(expectedPatch, tagValue.Patch);
            Assert.Equal(expectedFix, tagValue.Fix);
            Assert.Equal(expectedPrefix, tagValue.Prefix);
            Assert.Equal(expectedSuffix, tagValue.Suffix);
            Assert.Equal(inputValue, tagValue.StringValue);

        }
    }
}
