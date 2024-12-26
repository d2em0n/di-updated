using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.TextProcessor;

namespace TagsCloudContainer.TagGenerator
{
    public class RandomColorTagGenerator : ITagsGenerator
    {
        public IEnumerable<Tag> GetTags(ITextProcessor processor)
        {
            throw new NotImplementedException();
        }
    }
}
