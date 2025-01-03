using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.TextProcessor;

namespace TagsCloudContainer.TagGenerator
{
    public class SingleColorTagGenerator(ITextProcessor processor, Graphics graphics, Font defaultFont, Color color ): ITagsGenerator
    {
        public IEnumerable<Tag> GenerateTags()
        {
            return processor.Words()
                .Select(kvp => new Tag(graphics, kvp.Key, SetFont(defaultFont, kvp.Value), color));
        }

        private static Font SetFont(Font font, int amount)
        {
            return new Font(font.FontFamily, font.Size * amount);
        }
    }
}
