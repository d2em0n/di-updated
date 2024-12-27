using System.Text.RegularExpressions;
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
                if (filters.All(filter => !filter.Skips(word)))
                    continue;

                if (!words.ContainsKey(word))
                    words.Add(word, 0);
                words[word]++;
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
