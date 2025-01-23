using System.Drawing;

namespace TagsCloudContainer.ColorProviders;

public class RandomColorProvider : IColorProvider
{
    public Result<Color> GetColor()
    {
        return Color.FromArgb(Random.Shared.Next(50, 255), Random.Shared.Next(0, 255), Random.Shared.Next(0, 255), Random.Shared.Next(0, 255));
    }
}