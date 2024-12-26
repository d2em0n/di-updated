using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.TextProcessor
{
    public interface ITextProcessor
    {
        public Dictionary<Word, int> Words(string path, ITextProvider provider, params IWordFilter[] filters);
    }
}
