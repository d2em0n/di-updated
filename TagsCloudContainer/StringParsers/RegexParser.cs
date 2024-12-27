using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TagsCloudContainer.StringParsers
{
    public class RegexParser : IStringParser
    {
        public IEnumerable<Word> GetWordsFromString(string input)
        {
            var regex = new Regex("\\b(?:\\w|-)+\\b");

            return regex.Matches(input)
                .Cast<Match>()
                .Select(w => new Word(w.Value));
        }
    }
}
