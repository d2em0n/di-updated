using System.Drawing;
using TagsCloudContainer.PointGenerators;
using TagsCloudContainer.TagGenerator;
using TagsCloudContainer.TextProcessor;

namespace TagsCloudContainer;

public class PictureMaker
{
    private readonly IPointGenerator pointGenerator;
    private readonly IEnumerable<Tag> tags;
    private readonly string fileName;
    private readonly Point startPoint;

    public PictureMaker(IPointGenerator pointGenerator, ITagsGenerator tagGenerator,
        ITextProcessor textProcessor, string fileName, Point startPoint)
    {
        this.pointGenerator = pointGenerator;
        this.tags = tagGenerator.GenerateTags(textProcessor.WordFrequencies());
        this.fileName = fileName;
        this.startPoint = startPoint;
    }

    public void DrawPicture()
    {
        var layout = new CloudLayout(startPoint, pointGenerator);
        var image = new Bitmap(layout.Size.Width, layout.Size.Height);
        foreach (var tag in tags)
        {
            var rectangle = layout.PutNextRectangle(tag.Frame);
            DrawTag(image, rectangle, tag);
        }
        image.Save(fileName);
    }

    private static void DrawTag(Bitmap image, Rectangle rectangle, Tag tag)
    {
        var brush = new SolidBrush(tag.Color);
        var formGraphics = Graphics.FromImage(image);
        formGraphics.DrawString(tag.Word.Value, tag.Font, brush, rectangle.Location);
        brush.Dispose();
        formGraphics.Dispose();
    }
}