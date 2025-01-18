using FluentAssertions;
using TagsCloudContainer.TextProviders;

namespace TagsCloudContainer.Tests
{
    public class TxtTextProviderShould
    {
        private TxtTextProvider _provider;
        [SetUp]
        public void Setup()
        {
            _provider = new TxtTextProvider("NotExisted.txt");
        }

        [Test]
        public void ThrowExceptionIfFileNotFounded()
        {
            var result = _provider.ReadFile();

            result.Error.Should().Be("File NotExisted.txt does not exist");
        }
    }
}