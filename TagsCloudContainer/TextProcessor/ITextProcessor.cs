using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.TextProcessor
{
    public interface ITextProcessor
    {
        public Dictionary<Word, int> Words(string path, ITextProvider provider, IStringParser parser, params IWordFilter[] filters);
    }
}
