using FluentAssertions;
using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.Tests
{
    public class TextProcessorShould
    {
        [Test]
        public void Process()
        {
            var result = new TextProcessor.TextProcessor(
                new TxtTextProvider(@"TextFile1.txt"), new RegexParser(), new ToLowerFilter(), new BoringWordFilter(), new ShortWordFilter()).WordFrequencies();

            result.GetValueOrThrow().Count.Should().Be(3);

            result.GetValueOrThrow().MaxBy(word => word.Value).Value.Should().Be(3);
        }
    }
}
