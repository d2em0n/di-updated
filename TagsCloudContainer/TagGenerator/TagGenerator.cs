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
            var tagsResults = new List<Result<Tag>>();

            foreach (var kvp in wordsDictionary)
            {
                var colorResult = _colorProvider.GetColor();
                if (!colorResult.IsSuccess)
                    return Result.Fail<IEnumerable<Tag>>(colorResult.Error);

                var fontResult = SetFont(_defaultFont, kvp.Value);
                if (!fontResult.IsSuccess)
                    return Result.Fail<IEnumerable<Tag>>(fontResult.Error);

                var frameSizeResult = SetFrameSize(kvp.Key, fontResult.Value, 1, _graphics);
                if (!frameSizeResult.IsSuccess)
                    return Result.Fail<IEnumerable<Tag>>(frameSizeResult.Error);
               
                tagsResults.Add(Result.Ok(new Tag(kvp.Key, fontResult.Value, colorResult.Value, frameSizeResult.Value)));
            }
            return Result.Ok(tagsResults.Select(t => t.Value));
        }

        private static Result<Size> SetFrameSize(Word? word, Font? font, int frameGap, Graphics? graphics)
        {
            if (word == null)
                return Result.Fail<Size>("Word is null");
            if (font == null)
                return Result.Fail<Size>("Font is null");
            if (frameGap <= 0)
                return Result.Fail<Size>("Frame gap is lesser than 0");
            if (graphics == null)
                return Result.Fail<Size>("Graphics is null");

            var rect = graphics.MeasureString(word.Value, font).ToSize();
            return Result.Ok(new Size(rect.Width + frameGap, rect.Height + frameGap));
        }

        private static Result<Font> SetFont(Font? font, int amount)
        {
            if (amount <= 0)
                return Result.Fail<Font>("Amount must be greater than 0");
            return font == null ? Result.Fail<Font>("Font is null") : new Font(font.FontFamily, font.Size * amount).AsResult();
        }
    }
}
