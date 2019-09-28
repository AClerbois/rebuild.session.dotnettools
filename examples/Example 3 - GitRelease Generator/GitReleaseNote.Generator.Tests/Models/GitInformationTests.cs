using GitReleaseNote.Generator.Models;
using Xunit;

namespace GitReleaseNote.Generator.Tests.Models
{
    public class GitInformationTests
    {
        [Theory]
        [InlineData("0.7.1", "(tag: refs/tags/0.7.1)")]
        [InlineData("0.7.1", " (tag: refs/tags/0.7.1)")]
        [InlineData("hello-my-friend", " (tag: refs/tags/hello-my-friend)")]
        [InlineData("0.8.2", "(tag: refs/tags/0.8.2, refs/remotes/origin/feature/PAYSMB-7894-update-te)")]
        public void ExtractionOfOneTagName(string expected, string value)
        {
            // Arrange
            var gitInformation = new GitInformation();

            // Act
            gitInformation.SetTagName(value);

            // Assert
            Assert.Single(gitInformation.TagsName);
            Assert.Equal(expected, gitInformation.TagsName[0]);
        }

        [Theory]
        [InlineData("")]
        [InlineData("fake-data")]
        public void ExtractionOfWithoutAnyTags(string value)
        {
            // Arrange
            var gitInformation = new GitInformation();

            // Act
            gitInformation.SetTagName(value);

            // Assert
            Assert.Empty(gitInformation.TagsName);
        }

        [Theory]
        [InlineData("(tag: refs/tags/4.8.2, tag: refs/tags/4.8.1)", "4.8.1","4.8.2")]
        public void ExtractionOfWithMoreThanOne(string value, params string[] expectations)
        {
            // Arrange
            var gitInformation = new GitInformation();

            // Act
            gitInformation.SetTagName(value);

            // Assert
            Assert.Equal(expectations.Length, gitInformation.TagsName.Count);
            foreach (var expected in expectations)
            {
                Assert.Contains(expected, gitInformation.TagsName);
            }          
        }

        [Theory]
        [InlineData("ENTO-457", "refactor ResponseCommonAdapter + modifying tests in accordance", "ENTO-457: refactor ResponseCommonAdapter + modifying tests in accordance")]
        [InlineData("ENTO-494", "change temporary the connection string until the infra and ops create the real one", "ENTO-494 : change temporary the connection string until the infra and ops create the real one")]
        [InlineData("ENTO-647", "Cleaning startup", "ENTO-647 - Cleaning startup")]
        [InlineData("ENTO-647", "Cleaning startup", "ENTO-647- Cleaning startup")]
        public void ExtractionOfTheComment(string expectedJiraTicket, string expectedComment, string message)
        {
            // Arrange
            var gitInformation = new GitInformation();

            // Act
            gitInformation.SetComment(message);

            // Assert
            Assert.Equal(expectedJiraTicket, gitInformation.JiraTicket);
            Assert.Equal(expectedComment, gitInformation.Comment);
        }
    }
}
