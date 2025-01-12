using System.Drawing;
using System.Runtime.CompilerServices;

namespace TagsCloudContainer.ColorProviders;

public class ColorProvider : IColorProvider
{
    [CompilerGenerated] private readonly Color _color;

    public ColorProvider(Color color) => _color = color;

    public Color GetColor() => _color;
}