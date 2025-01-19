using System.Drawing;
using TagsCloudContainer.ColorProviders;

namespace TagsCloudContainer.TagGenerator
{
    public class TagGenerator : ITagsGenerator
    {
        private readonly IColorProvider _colorProvider;
        private readonly Graphics _graphics;
        private readonly Font _defaultFont;

       
        public TagGenerator(IColorProvider colorProvider, Font defaultFont )
        {
            _colorProvider = colorProvider;
            _graphics = Graphics.FromImage(new Bitmap(1, 1));
            _defaultFont = defaultFont;
        }

        public Result<IEnumerable<Tag>> GenerateTags(Result<Dictionary<Word, int>> wordsDictionary)
        {
            if (!wordsDictionary.IsSuccess)
                return Result.Fail<IEnumerable<Tag>>(wordsDictionary.Error);
            var tags = wordsDictionary.Value
                .Select(kvp => new Tag(kvp.Key, SetFont(_defaultFont, kvp.Value), _colorProvider.GetColor(),
                    SetFrameSize(kvp.Key, SetFont(_defaultFont, kvp.Value), 1, _graphics)));
            return Result.Ok(tags);
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
