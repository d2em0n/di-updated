using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.WordFilters;

public class ToLowerFilter : IWordFilter
{
    public IEnumerable<Word> Process(IEnumerable<Word> words)
    {
        return words.Select(w => new Word(w.Value.ToLower()));
    }
}