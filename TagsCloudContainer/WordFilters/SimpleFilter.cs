using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.WordFilters
{
    public class SimpleFilter : IWordFilter
    {
        private readonly HashSet<string> _words =
        [
            "я",
            "мы",
            "он",
            "она",
            "оно",
            "они"
        ];
        public bool Skips(Word word)
        {
            return !_words.Contains(word.Value);
        }

        public void AddBoringWord(Word word)
        {
            _words.Add(word.Value);
        }

        public void AddBoringWord(string word)
        {
            _words.Add(word);
        }
    }
}
