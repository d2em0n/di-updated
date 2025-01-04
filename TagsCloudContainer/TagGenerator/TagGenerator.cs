using System.Drawing;
using System.Linq;
using TagsCloudContainer.ColorProviders;
using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProcessor;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.TagGenerator
{
    public class TagGenerator : ITagsGenerator
    {
        private readonly IColorProvider _colorProvider;
        private readonly Graphics _graphics;
        private readonly Font _defaultFont;

       
        public TagGenerator(IColorProvider colorProvider, Graphics graphics, Font defaultFont )
        {
            _colorProvider = colorProvider;
            _graphics = graphics;
            _defaultFont = defaultFont;
        }

        public IEnumerable<Tag> GenerateTags(Dictionary<Word, int> wordsDictionary)
        {
            return wordsDictionary
                .Select(kvp => new Tag(kvp.Key, SetFont(_defaultFont, kvp.Value), _colorProvider.GetColor(),
                    SetFrameSize(kvp.Key, _defaultFont, 1, _graphics)));
        }

        private static Size SetFrameSize(Word word, Font font, int frameGap, Graphics graphics)
        {
            var rect = graphics.MeasureString(word.Value, font).ToSize();
            return new Size(rect.Width + frameGap, rect.Height + frameGap);
        }

        private static Font SetFont(Font font, int amount)
        {
            return new Font(font.FontFamily, font.Size * amount);
        }
    }
}
