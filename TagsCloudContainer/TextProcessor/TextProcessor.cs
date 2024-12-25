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
        public readonly Dictionary<Word, int> Words;
        public TextProcessor(string path, ITextProvider provider, params IWordFilter[] filters)
        {
            Words = [];
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
                if (!Words.TryGetValue(word, out var value))
                    Words.Add(word, 1);
                else
                    Words[word] += 1;
            }
        }

        private static IEnumerable<Word> GetWordsFromString(string input)
        {
            return Regex.Split(input, @"\W+")
                .Select(s => new Word(s));
        }
    }
}
