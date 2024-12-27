using System.Text.RegularExpressions;
using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.TextProcessor
{
    public class TextProcessor : ITextProcessor
    {
        public Dictionary<Word, int> Words(string path, ITextProvider provider, IStringParser parser, params IWordFilter[] filters)
        {
            var words = new Dictionary<Word, int>();
            foreach (var word in parser.GetWordsFromString(provider.ReadFile(path)))
            {
                if (filters.All(filter => !filter.Skips(word)))
                    continue;

                if (!words.ContainsKey(word))
                    words.Add(word, 0);
                words[word]++;
            }
            return words;
        }
    }
}
