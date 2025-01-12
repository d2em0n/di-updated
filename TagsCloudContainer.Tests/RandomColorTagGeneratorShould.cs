using FluentAssertions;
using System.Drawing;
using TagsCloudContainer.ColorProviders;
using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.Tests
{
    public class RandomColorTagGeneratorShould
    {
        [Test]
        public void SetRightFontSize()
        {
            var bitmap = new Bitmap(1, 1);
            using var graphics = Graphics.FromImage(bitmap);

            var processor = new TextProcessor.TextProcessor(
                new TxtTextProvider(@"TextFile1.txt"),  new RegexParser(), new ToLowerFilter(), new BoringWordFilter());
            var words = processor.WordFrequencies();
            var generator = new TagGenerator.TagGenerator(new RandomColorProvider(),  new Font("arial", 12));
            var result = generator.GenerateTags(words).First();
            
            result.Font.Name.Should().Be("Arial");
            result.Font.Size.Should().Be(36);
        }
    }
}
