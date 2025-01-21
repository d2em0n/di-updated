using System.Drawing;

namespace TagsCloudContainer.ColorProviders;

public interface IColorProvider
{
    Result<Color> GetColor();
}