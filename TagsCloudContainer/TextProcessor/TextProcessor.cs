using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.TextProcessor
{
    public class TextProcessor : ITextProcessor
    {
        public Dictionary<Word, int> Words(string path, ITextProvider provider, params IWordFilter[] filters)
        {
            var words = new Dictionary<Word, int>();
            foreach (var word in GetWordsFromString(provider.ReadFile(path)))
            {
                var flag = true;
                foreach (var filter in filters)
                {
                    if (flag == false) break;
                    if (filter.Skips(word)) continue;
                    flag = false;
                }
                if (flag == false) continue;
                if (!words.TryGetValue(word, out var _))
                    words.Add(word, 1);
                else
                    words[word] += 1;
            }
            return words;
        }

        private static IEnumerable<Word> GetWordsFromString(string input)
        {
            var regex = new Regex("\\b(?:\\w|-)+\\b");

            return regex.Matches(input)
                .Cast<Match>()
                .Select(w => new Word(w.Value));
        }
    }
}
