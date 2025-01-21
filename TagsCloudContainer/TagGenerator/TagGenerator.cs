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
                    var colorResult = _colorProvider.GetColor();
                    if (!colorResult.IsSuccess) 
                        return Result.Fail<Tag>(colorResult.Error);
                    
                    var fontResult = Result.Of(() => SetFont(_defaultFont, kvp.Value));
                    if (!fontResult.IsSuccess)
                        return Result.Fail<Tag>(fontResult.Error);
                    
                    var frameSizeResult = Result.Of(() => SetFrameSize(kvp.Key, fontResult.Value, 1, _graphics));
                    if (!frameSizeResult.IsSuccess)
                        return Result.Fail<Tag>(frameSizeResult.Error);

                    return Result.Ok(new Tag(kvp.Key, SetFont(_defaultFont, kvp.Value), colorResult.Value,
                        SetFrameSize(kvp.Key, SetFont(_defaultFont, kvp.Value), 1, _graphics)));
                });
            return Result.Ok(tagsResult.Select(t => t.Value))
                .OnFail(error => Result.Fail<Tag>(error));
        }

        private static Size SetFrameSize(Word word, Font font, int frameGap, Graphics graphics)
        {
            ArgumentNullException.ThrowIfNull(word);
            ArgumentNullException.ThrowIfNull(font);
            ArgumentNullException.ThrowIfNull(graphics);

            var rect = graphics.MeasureString(word.Value, font).ToSize();
            return new Size(rect.Width + frameGap, rect.Height + frameGap);
        }

        private static Font SetFont(Font font, int amount)
        {
            ArgumentNullException.ThrowIfNull(font);
            if (amount <= 0) throw new ArgumentException(null, nameof(amount));
            
            return new Font(font.FontFamily, font.Size * amount);
        }
    }
}
