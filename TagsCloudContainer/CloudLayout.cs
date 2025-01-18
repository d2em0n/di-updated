using System.Drawing;
using TagsCloudContainer.PointGenerators;

namespace TagsCloudContainer
{
    public class CloudLayout
    {
        private readonly Point Center;
        public readonly Size Size;
        private readonly IEnumerable<Point> _points;
        private List<Rectangle> Rectangles { get; set; }
        private readonly Rectangle _frame;


        public CloudLayout(Point center, IPointGenerator pointGenerator)
        {
            if (center.X <= 0 || center.Y <= 0)
                throw new ArgumentException("Center coordinates values have to be greater than Zero");
            Center = center;
            Size = CountSize(center);
            Rectangles = [];
            _points = pointGenerator.GeneratePoints(Center);
            _frame = new Rectangle(0, 0, Size.Width, Size.Height);
        }

        public CloudLayout(Size size, IPointGenerator pointGenerator)
        {
            Size = size;
            Center = FindCenter(size);
            Rectangles = [];
            _points = pointGenerator.GeneratePoints(Center);
            _frame = new Rectangle(0, 0, Size.Width, Size.Height);
        }


        private Size CountSize(Point center)
        {
            var width = (center.X % 2 == 0) ? center.X * 2 + 1 : Center.X * 2;
            var height = (center.Y % 2 == 0) ? center.Y * 2 + 1 : center.Y * 2;
            return new Size(width, height);
        }

        private static Point FindCenter(Size size)
        {
            return new Point(size.Width / 2, size.Height / 2);
        }

        public Result<Rectangle> PutNextRectangle(Size rectangleSize)
        {
            foreach (var point in _points)
            {
                var supposed = new Rectangle(new Point(point.X - rectangleSize.Width / 2, point.Y - rectangleSize.Height / 2),
                    rectangleSize);
                if (IntersectsWithAnyOther(supposed, Rectangles))
                    continue;
                if (_frame.Contains(supposed))
                {
                    Rectangles.Add(supposed);
                    return supposed;
                }

                return Result.Fail<Rectangle>("Вышли за границы рисунка");
            }
            throw new ArgumentException("Not Enough Points Generated");
        }

        public static bool IntersectsWithAnyOther(Rectangle supposed, List<Rectangle> others)
        {
            return others.Any(x => x.IntersectsWith(supposed));
        }
    }
}

