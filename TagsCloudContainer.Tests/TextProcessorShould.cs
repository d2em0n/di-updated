using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.Tests
{
    public class TextProcessorShould
    {
        [Test]
        public void Process()
        {
            var processor = new TextProcessor.TextProcessor(@"TextFile1.txt",
                new TxtTextProvider(), new SimpleFilter());

            processor.Words.Count.Should().Be(3);

            processor.Words.MaxBy(word => word.Value).Value.Should().Be(3);
        }
    }
}
