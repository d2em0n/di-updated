using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.TextProcessor;

namespace TagsCloudContainer.TagGenerator
{
    public interface ITagsGenerator
    {
        IEnumerable<Tag> GetTags(ITextProcessor processor);
    }
}
