using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.StringParsers
{
    public interface IStringParser
    {
        public IEnumerable<Word> GetWordsFromString(string input);
    }
}
