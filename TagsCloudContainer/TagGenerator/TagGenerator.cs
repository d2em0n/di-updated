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

        public Result<IEnumerable<Tag>> GenerateTags(Dictionary<Word, int> wordsDictionary)
        {
            var tagsResult =  wordsDictionary
                .Select(kvp =>
                {
                    var color = _colorProvider.GetColor();
                    if (!color.IsSuccess) return Result.Fail<Tag>(color.Error);

                    return Result.Ok(new Tag(kvp.Key, SetFont(_defaultFont, kvp.Value), color.Value,
                        SetFrameSize(kvp.Key, SetFont(_defaultFont, kvp.Value), 1, _graphics)));
                });
            return Result.Ok(tagsResult.Select(t => t.Value))
                .OnFail(error => Result.Fail<Tag>(error));
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
