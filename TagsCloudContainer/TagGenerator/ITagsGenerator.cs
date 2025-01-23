namespace TagsCloudContainer.TagGenerator
{
    public interface ITagsGenerator
    {
        Result<IEnumerable<Tag>> GenerateTags(Dictionary<Word, int> wordsDictionary);
    }
}
