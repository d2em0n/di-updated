using System.Text.RegularExpressions;
using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.TextProcessor
{
    public class TextProcessor(string path, ITextProvider provider, IStringParser parser,
        params IWordFilter[] filters) : ITextProcessor
    {
        public Dictionary<Word, int> WordFrequencies()
        {
            var words = new Dictionary<Word, int>();
            foreach (var word in parser.GetWordsFromString(provider.ReadFile(path)))
            {
                if (filters.Any(filter => !filter.Skips(word)))
                    continue;

                if (!words.ContainsKey(word))
                    words.Add(word, 0);
                words[word]++;
            }
            return words;
        }
    }
}
