using System.Text.RegularExpressions;
using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.TextProcessor;

public class TextProcessor(ITextProvider provider, IStringParser parser,
    params IWordFilter[] filters) : ITextProcessor
{
    public Dictionary<Word, int> WordFrequencies()
    {
        var words = parser.GetWordsFromString(provider.ReadFile());
        return filters.Aggregate(words, (current, filter) => filter.Process(current))
            .GroupBy(word => word)
            .ToDictionary(group => group.Key, group => group.Count());
    }
}