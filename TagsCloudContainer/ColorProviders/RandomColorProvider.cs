using System.Drawing;

namespace TagsCloudContainer.ColorProviders;

public class RandomColorProvider : IColorProvider
{
    private static readonly Random Random = new();
    public Color GetColor()
    {
        return Color.FromArgb(Random.Next(50, 255), Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255));
    }
}