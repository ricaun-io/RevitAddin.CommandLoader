using NUnit.Framework;
using RevitAddin.CommandLoader.Services;
using System;

namespace RevitAddin.CommandLoader.Tests
{
    public class GistGithubUtilsTests
    {
        private const string GIST_SOURCE = "https://gist.github.com/ricaun/200a576c3baa45cba034ceedac1e708e";

        [Test]
        public void Test_TryGetGistFilesContent()
        {
            var hasGistContent = GistGithubUtils.TryGetGistFilesContent(GIST_SOURCE, out string[] contents);
            Assert.IsTrue(hasGistContent, "Gist content not found.");
        }

        [Test]
        public void Test_TryGetGistModel()
        {
            var hasGistContent = GistGithubUtils.TryGetGistModel(GIST_SOURCE, out var model);
            Assert.IsTrue(hasGistContent, "Gist content not found.");
        }

        [Test]
        public void Test_TryGetGistId()
        {
            var hasGistContent = GistGithubUtils.TryGetGistId(GIST_SOURCE, out var id);
            Assert.IsTrue(hasGistContent, "Gist content not found.");
            Console.WriteLine(id);
        }

        [Test]
        public void Test_TryGetGistString()
        {
            var hasGistContent = GistGithubUtils.TryGetGistString(GIST_SOURCE, out string content);
            Assert.IsTrue(hasGistContent, "Gist content not found.");
        }

    }
}