using GitReleaseNote.Generator.Services;
using Xunit;

namespace GitReleaseNote.Generator.Tests.Services
{
    public class GitVersionConverterTests
    {
        [Fact]
        public void Convert_WithOrderedLogsList_ExpectReturnStructuredList()
        {
            // Arrange
            var sut = new GitVersionConverter();
            string values = "1.0.0\n1.1.0\n1.2.0\n1.3.0\n1.5.0\n1.6.0\n2.0.0\n3.0.0\n3.1.0\n";

            // Act
            var result = sut.Convert(values);

            // Assert
            Assert.Equal(9, result.Count);
            Assert.Equal("1.0.0", result[0]);
            Assert.Equal("1.1.0", result[1]);
            Assert.Equal("1.2.0", result[2]);
            Assert.Equal("1.3.0", result[3]);
            Assert.Equal("1.5.0", result[4]);
            Assert.Equal("1.6.0", result[5]);
            Assert.Equal("2.0.0", result[6]);
            Assert.Equal("3.0.0", result[7]);
            Assert.Equal("3.1.0", result[8]);
        }

        [Fact]
        public void Convert_WithUnorderedLogsList_ExpectReturnStructuredList()
        {
            // Arrange
            var sut = new GitVersionConverter();
            string values = "1.0.0\n1.1.1\n1.1.10\n1.1.11\n1.1.2\n1.1.20\n1.1.21\n1.1.3\n3.1.0\n";

            // Act
            var result = sut.Convert(values);

            // Assert
            Assert.Equal(9, result.Count);
            Assert.Equal("1.0.0", result[0]);
            Assert.Equal("1.1.1", result[1]);
            Assert.Equal("1.1.2", result[2]);
            Assert.Equal("1.1.3", result[3]);
            Assert.Equal("1.1.10", result[4]);
            Assert.Equal("1.1.11", result[5]);
            Assert.Equal("1.1.20", result[6]);
            Assert.Equal("1.1.21", result[7]);
            Assert.Equal("3.1.0", result[8]);
        }

        [Fact]
        public void Convert_WithUnorderedLogsListContainingPreRelease_ExpectReturnStructuredList()
        {
            // Arrange
            var sut = new GitVersionConverter();
            string values = "1.0.0\n1.1.1\n1.1.10\n1.1.11\n1.1.2\n1.1.2-pre1\n1.1.2-pre2\n1.1.3\n3.1.0\n";

            // Act
            var result = sut.Convert(values);

            // Assert
            Assert.Equal(9, result.Count);
            Assert.Equal("1.0.0", result[0]);
            Assert.Equal("1.1.1", result[1]);
            Assert.Equal("1.1.2-pre1", result[2]);
            Assert.Equal("1.1.2-pre2", result[3]);
            Assert.Equal("1.1.2", result[4]);
            Assert.Equal("1.1.3", result[5]);
            Assert.Equal("1.1.10", result[6]);
            Assert.Equal("1.1.11", result[7]);
            Assert.Equal("3.1.0", result[8]);
        }

        [Fact]
        public void Convert_WithUnorderedLogsListContainingPreReleaseAndPrefix_ExpectReturnStructuredList()
        {
            // Arrange
            var sut = new GitVersionConverter();
            string values = "v1.0.0\nv1.1.1\nv1.1.10\nv1.1.11\nv1.1.2\nv1.1.2-pre1\nv1.1.2-pre2\nv1.1.3\nv3.1.0\n";

            // Act
            var result = sut.Convert(values);

            // Assert
            Assert.Equal(9, result.Count);
            Assert.Equal("v1.0.0", result[0]);
            Assert.Equal("v1.1.1", result[1]);
            Assert.Equal("v1.1.2-pre1", result[2]);
            Assert.Equal("v1.1.2-pre2", result[3]);
            Assert.Equal("v1.1.2", result[4]);
            Assert.Equal("v1.1.3", result[5]);
            Assert.Equal("v1.1.10", result[6]);
            Assert.Equal("v1.1.11", result[7]);
            Assert.Equal("v3.1.0", result[8]);
        }

        [Fact]
        public void Convert_WithUnorderedLogsListContainingPreReleaseAndPrefixes_ExpectReturnStructuredList()
        {
            // Arrange
            var sut = new GitVersionConverter();
            string values = "v1.0.0\nv1.1.1\nv1.1.10\nv1.1.11\nv1.1.2\nv1.1.2-pre1\na1.1.2-pre2\nv1.1.3\nv3.1.0\n";

            // Act
            var result = sut.Convert(values);

            // Assert
            Assert.Equal(9, result.Count);
            Assert.Equal("a1.1.2-pre2", result[0]);
            Assert.Equal("v1.0.0", result[1]);
            Assert.Equal("v1.1.1", result[2]);
            Assert.Equal("v1.1.2-pre1", result[3]);
            Assert.Equal("v1.1.2", result[4]);
            Assert.Equal("v1.1.3", result[5]);
            Assert.Equal("v1.1.10", result[6]);
            Assert.Equal("v1.1.11", result[7]);
            Assert.Equal("v3.1.0", result[8]);
        }

        [Fact]
        public void Convert_WithOrderedLogsListContainingFix_ExpectReturnStructuredList()
        {
            // Arrange
            var sut = new GitVersionConverter();
            string values = "1.0.0\n1.0.0.1\n1.2.0\n1.3.0\n1.5.0\n1.6.0\n2.0.0\n3.0.0\n3.1.0\n";

            // Act
            var result = sut.Convert(values);

            // Assert
            Assert.Equal(9, result.Count);
            Assert.Equal("1.0.0", result[0]);
            Assert.Equal("1.0.0.1", result[1]);
            Assert.Equal("1.2.0", result[2]);
            Assert.Equal("1.3.0", result[3]);
            Assert.Equal("1.5.0", result[4]);
            Assert.Equal("1.6.0", result[5]);
            Assert.Equal("2.0.0", result[6]);
            Assert.Equal("3.0.0", result[7]);
            Assert.Equal("3.1.0", result[8]);
        }


    }
}
