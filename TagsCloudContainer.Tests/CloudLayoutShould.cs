using FluentAssertions;
using System.Drawing;
using TagsCloudContainer.PointGenerators;


namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class CloudLayoutShould
    {
        private CloudLayout layout;
        [TestCase(1, 2, TestName = "Odd coordinate value results in an even size value")]
        [TestCase(2, 5, TestName = "Even coordinate value results in an odd size value")]
        public void MakeRightSizeLayout(int coordinateValue, int sizeValue)
        {
            var center = new Point(coordinateValue, coordinateValue);
            var size = new Size(sizeValue, sizeValue);

            layout = new CloudLayout(center, new ArchemedianSpiral());

            layout.Size.Should().BeEquivalentTo(size);
        }

        [TestCase(-1, 1, TestName = "Negative X")]
        [TestCase(1, -1, TestName = "Negative Y")]
        [TestCase(0, 1, TestName = "Zero X")]
        [TestCase(1, 0, TestName = "Zero Y")]
        public void GetOnlyPositiveCenterCoordinates(int x, int y)
        {
            Action makeLayout = () => new CloudLayout(new Point(x, y), new ArchemedianSpiral());

            makeLayout.Should().Throw<ArgumentException>()
                .WithMessage("Center coordinates values have to be greater than Zero");
        }

        [Test]
        public void PutNextRectangle_ShouldKeepEnteredSize()
        {
            layout = new CloudLayout(new Point(5, 5), new ArchemedianSpiral());
            var enteredSize = new Size(3, 4);
            var returnedSize = layout.PutNextRectangle(enteredSize).Size;

            returnedSize.Should().BeEquivalentTo(enteredSize);
        }
    }
}
