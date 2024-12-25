using TagsCloudContainer.TextProviders;
using FluentAssertions;

namespace TagsCloudContainer.Tests
{
    public class TxtTextProviderShould
    {
        private ITextProvider _provider;
        [SetUp]
        public void Setup()
        {
            _provider = new TxtTextProvider();
        }

        [Test]
        public void ThrowException()
        {
            Action act = () => _provider.ReadFile("123.txt");

            act.Should().Throw<FileNotFoundException>();
        }
    }
}