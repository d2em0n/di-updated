using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.WordFilters
{
    public class ShortWordFilter : IWordFilter
    {
        public bool Skips(Word word)
        {
            return word.Value.Length > 2;
        }
    }
}
