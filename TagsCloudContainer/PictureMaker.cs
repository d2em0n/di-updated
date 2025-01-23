using System.Drawing;
using TagsCloudContainer.PointGenerators;
using TagsCloudContainer.TagGenerator;
using TagsCloudContainer.TextProcessor;

namespace TagsCloudContainer;

public class PictureMaker
{
    private readonly IPointGenerator _pointGenerator;
    private readonly ITagsGenerator _tagGenerator;
    private readonly ITextProcessor _textProcessor;
    private readonly string _fileName;
    private readonly Point _startPoint;

    public PictureMaker(IPointGenerator pointGenerator, ITagsGenerator tagGenerator,
        ITextProcessor textProcessor, string fileName, Point startPoint)
    {
        _pointGenerator = pointGenerator;
        _tagGenerator = tagGenerator;
        _textProcessor = textProcessor;
        _fileName = fileName;
        _startPoint = startPoint;
    }

    public Result<None> DrawPicture()
    {
        var layout = new CloudLayout(_startPoint, _pointGenerator);
        using var image = new Bitmap(layout.Size.Width, layout.Size.Height);

        return _textProcessor.WordFrequencies()
            .Then(wordsDict => _tagGenerator.GenerateTags(wordsDict))
            .ForEach(tag =>
            {
                var rectange = layout.PutNextRectangle(tag.Frame)
                    .Then(rect => DrawTag(image, rect, tag))
                    .Then(_ => image.Save(_fileName));
            });
    }

    private static void DrawTag(Bitmap image, Rectangle rectangle, Tag tag)
    {
        using var brush = new SolidBrush(tag.Color);
        using var formGraphics = Graphics.FromImage(image);
        formGraphics.DrawString(tag.Word.Value, tag.Font, brush, rectangle.Location);
    }
}