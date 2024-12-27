using System.Drawing;
using System.Linq;
using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProcessor;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.TagGenerator
{
    public class RandomColorTagGenerator(ITextProcessor processor, Graphics graphics, Font defaultFont) : ITagsGenerator
    {
        private static readonly Random Random = new();

        public IEnumerable<Tag> GenerateTags()
        {
            return processor.Words()
                .Select(kvp => new Tag(graphics, kvp.Key, SetFont(defaultFont, kvp.Value), GetRandomColor()));
        }


        private static Font SetFont(Font font, int amount)
        {
            return new Font(font.FontFamily, font.Size * amount);
        }

        private static Color GetRandomColor()
        {
            return Color.FromArgb(Random.Next(50, 255), Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255));
        }
    }
}
