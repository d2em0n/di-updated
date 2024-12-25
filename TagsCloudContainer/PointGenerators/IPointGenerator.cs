using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.PointGenerators
{
    public interface IPointGenerator
    {
        IEnumerable<Point> GeneratePoints(Point start);
    }
}
