using TagsCloudContainer.TextProcessor;

namespace TagsCloudContainer.TagGenerator
{
    public interface ITagsGenerator
    {
        IEnumerable<Tag> GetTags(ITextProcessor processor);
    }
}
