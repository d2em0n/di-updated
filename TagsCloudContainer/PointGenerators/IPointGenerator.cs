using System.Drawing;

namespace TagsCloudContainer.PointGenerators
{
    public interface IPointGenerator
    {
        IEnumerable<Point> GeneratePoints(Point start);
    }
}
