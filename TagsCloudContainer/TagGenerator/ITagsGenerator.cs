namespace TagsCloudContainer.TagGenerator
{
    public interface ITagsGenerator
    {
        IEnumerable<Tag> GenerateTags(Dictionary<Word, int> wordsDictionary);
    }
}
