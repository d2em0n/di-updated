using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.TextProcessor;

public class TextProcessor(ITextProvider provider, IStringParser parser,
    params IWordFilter[] filters) : ITextProcessor
{
    public Result<Dictionary<Word, int>> WordFrequencies()
    {
        return provider.ReadFile()
            .Then(text => parser.GetWordsFromString(text))
            .Then(words => filters.Aggregate(words, (current, filter) => filter.Process(current))
        .GroupBy(word => word)
        .ToDictionary(group => group.Key, group => group.Count()));
        
    }
}