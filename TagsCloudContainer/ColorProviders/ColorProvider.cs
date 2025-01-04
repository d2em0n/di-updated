using System.Drawing;

namespace TagsCloudContainer.ColorProviders;

public class ColorProvider(Color color) : IColorProvider
{
    public Color GetColor()
    {
        return color;
    }
}