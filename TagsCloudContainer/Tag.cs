using System.Drawing;

namespace TagsCloudContainer;

public class Tag
{
    public readonly string Value;
    public readonly Font Font;
    public readonly Color Color;
    public readonly Size Frame;

    public Tag(Graphics g, Word word, Font font, Color color)
    {
        Value = word.Value;
        Font = font;
        Color = color;
        var rect = g.MeasureString(Value, Font).ToSize();
        Frame = new Size(rect.Width + 2, rect.Height + 2);
    }
}