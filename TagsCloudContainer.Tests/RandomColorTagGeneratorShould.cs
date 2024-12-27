using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TagGenerator;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.Tests
{
    public class RandomColorTagGeneratorShould
    {
        [Test]
        public void SetRightFontSize()
        {
            var bitmap = new Bitmap(1, 1);
            using var graphics = Graphics.FromImage(bitmap);

            var processor = new TextProcessor.TextProcessor(@"TextFile1.txt",
                new TxtTextProvider(), new RegexParser(), new BoringWordFilter());
            var generator = new RandomColorTagGenerator(processor, graphics, new Font("arial", 12));
            var result = generator.GenerateTags().First();

            
            result.Font.Name.Should().Be("Arial");
            result.Font.Size.Should().Be(36);
        }
    }
}
