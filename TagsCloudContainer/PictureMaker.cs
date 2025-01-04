using System.Drawing;
using TagsCloudContainer.PointGenerators;

namespace TagsCloudContainer;

public class PictureMaker
{
    public void DrawPicture(IPointGenerator pointGenerator, IEnumerable<Tag> tags, string filename, Point startPoint)
    {
        var layout = new CloudLayout(startPoint, pointGenerator);
        var image = new Bitmap(layout.Size.Width, layout.Size.Height);
        foreach (var tag in tags)
        {
            var rectangle = layout.PutNextRectangle(tag.Frame);
            DrawTag(image, rectangle, tag);
        }
        image.Save(filename);
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