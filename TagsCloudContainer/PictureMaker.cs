using System.Drawing;
using TagsCloudContainer.PointGenerators;
using TagsCloudContainer.TagGenerator;
using TagsCloudContainer.TextProcessor;

namespace TagsCloudContainer;

public class PictureMaker
{
    private readonly IPointGenerator _pointGenerator;
    private readonly IEnumerable<Tag> _tags;
    private readonly string _fileName;
    private readonly Point _startPoint;

    public PictureMaker(IPointGenerator pointGenerator, ITagsGenerator tagGenerator,
        ITextProcessor textProcessor, string fileName, Point startPoint)
    {
        _pointGenerator = pointGenerator;
        _tags = tagGenerator.GenerateTags(textProcessor.WordFrequencies());
        _fileName = fileName;
        _startPoint = startPoint;
    }

    public void DrawPicture()
    {
        var layout = new CloudLayout(_startPoint, _pointGenerator);
        using var image = new Bitmap(layout.Size.Width, layout.Size.Height);
        foreach (var tag in _tags)
        {
            var rectangle = layout.PutNextRectangle(tag.Frame);
            DrawTag(image, rectangle.GetValueOrThrow(), tag);
        }
        image.Save(_fileName);
    }

    private static void DrawTag(Bitmap image, Rectangle rectangle, Tag tag)
    {
        using var brush = new SolidBrush(tag.Color);
        using var formGraphics = Graphics.FromImage(image);
        formGraphics.DrawString(tag.Word.Value, tag.Font, brush, rectangle.Location);
    }
}