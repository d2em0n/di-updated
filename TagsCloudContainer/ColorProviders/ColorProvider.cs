using System.Drawing;

namespace TagsCloudContainer.ColorProviders;

public class ColorProvider : IColorProvider
{
    private readonly Color _color;

    public ColorProvider(Color color) => _color = color;

    public Result<Color> GetColor() => _color.AsResult();
}