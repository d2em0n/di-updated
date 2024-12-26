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
            var result = new TextProcessor.TextProcessor().Words(@"TextFile1.txt",
                new TxtTextProvider(), new SimpleFilter());

            result.Count.Should().Be(3);

            result.MaxBy(word => word.Value).Value.Should().Be(3);
        }
    }
}
