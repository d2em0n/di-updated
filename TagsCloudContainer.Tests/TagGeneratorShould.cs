using FluentAssertions;
using TagsCloudContainer.ColorProviders;
using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.Tests
{
    public class TagGeneratorShould
    {
        [Test]
        public void SetRightFontSize()
        {
            var words = new Dictionary<Word, int>()
            {
                {new Word("a"), 3}
            };
            var generator = new TagGenerator.TagGenerator(new RandomColorProvider(),  new System.Drawing.Font("arial", 12));
            var result = generator.GenerateTags(words).First();
            
            result.Font.Name.Should().Be("Arial");
            result.Font.Size.Should().Be(36);
        }
    }
}
