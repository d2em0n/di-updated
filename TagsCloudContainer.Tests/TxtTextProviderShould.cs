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
            _provider = new TxtTextProvider();
        }

        [Test]
        public void ThrowExceptionIfFileNotFounded()
        {
            Action act = () => _provider.ReadFile("NotExisted.txt");

            act.Should().Throw<FileNotFoundException>();
        }

        [Test]
        public void ReturnLowerCase()
        {
            var result = _provider.ReadFile("TextFile1.txt");

            foreach (var c in result.Where(c => char.IsLetter(c)))
                char.IsLower(c).Should().BeTrue();
        }
    }
}