using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProcessor;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer.TagGenerator
{
    public interface ITagsGenerator
    {
        IEnumerable<Tag> GenerateTags(Dictionary<Word, int> wordsDictionary);
    }
}
